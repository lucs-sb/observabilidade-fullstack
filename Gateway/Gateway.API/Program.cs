using FluentValidation;
using FluentValidation.AspNetCore;
using Gateway.API.Extensions;
using Gateway.API.Mappers;
using Gateway.API.Middleware;
using Gateway.CrossCutting.IoC;
using Gateway.Domain.Interfaces.Http;
using Gateway.Infrastructure.Integrations.Client;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Npgsql;
using OpenTelemetry.Logs;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
}).ConfigureApiBehaviorOptions(options =>
{
    options.InvalidModelStateResponseFactory = context => new CustomInvalidModelError().CustomErrorResponse(context);
});

builder.Services
  .AddHttpClient<IMicroApiClient, MicroApiClient>(client =>
  {
      client.DefaultRequestHeaders.Accept.Clear();
      client.DefaultRequestHeaders.Add("Accept", "application/json");
  });

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddOpenTelemetry()
    .ConfigureResource(res => res.AddService(builder.Environment.ApplicationName)) // Nomeia o serviço
    .WithMetrics(metricsBuilder => {
        metricsBuilder
            .AddAspNetCoreInstrumentation()
            .AddHttpClientInstrumentation()
            // Metrics provides by ASP.NET Core in .NET 8
            .AddMeter("Microsoft.AspNetCore.Hosting")
            .AddMeter("Microsoft.AspNetCore.Server.Kestrel")
            // Metrics provided by System.Net libraries
            .AddMeter("System.Net.Http")
            .AddMeter("System.Net.NameResolution")
            .AddPrometheusExporter();
    })
    .WithTracing(tracingBuilder => {
        tracingBuilder
            .AddAspNetCoreInstrumentation()
            .AddHttpClientInstrumentation()
            .AddEntityFrameworkCoreInstrumentation() // se usar EF Core
            .AddNpgsql() // instrumenta chamadas ao PostgreSQL via Npgsql
            .AddOtlpExporter(opt => opt.Endpoint = new Uri("http://jaeger:4317")); // exporta traces via OTLP para Jaeger
    });

// Dependences Injections
builder.Services.AddConfigurationDependencies(builder.Configuration);
builder.Services.AddApplicationDI();
builder.Services.AddAInfrastructureDI();

//FluentValidation
builder.Services.AddFluentValidationAutoValidation()
    .AddFluentValidationClientsideAdapters()
    .AddValidatorsFromAssemblyContaining<Program>();

//Mapster
builder.Services.RegisterMaps();

builder.Services.AddAuthentication(authOptions =>
{
    authOptions.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    authOptions.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        ValidateLifetime = true,
        IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(builder.Configuration["AuthSettings:SecretKey"]!)),
        ValidateIssuer = false,
        ValidateAudience = false
    };
});

builder.Services.AddAuthorization();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseMiddleware<ErrorHandlingGatewayMiddleware>();

app.MapControllers();

app.MapPrometheusScrapingEndpoint();

app.Run();
