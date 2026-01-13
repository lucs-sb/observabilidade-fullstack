using Elastic.Apm.NetCoreAll;
using Elastic.Ingest.Elasticsearch.DataStreams;
using Elastic.Serilog.Sinks;
using Elastic.Transport;
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
using Serilog;
using System.Text.Json.Serialization;

var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production";

Log.Logger = new LoggerConfiguration()
    .Enrich.FromLogContext()
    .Enrich.WithMachineName()
    .Enrich.WithThreadId()
    .Enrich.WithProperty("Application", "gateway-api")
    .WriteTo.Console()
    .WriteTo.Elasticsearch(
        new[] { new Uri("http://elasticsearch:9200") },
        opts =>
        {
            opts.DataStream = new DataStreamName("logs", "gateway-api", environment.ToLower().Replace(".", "-"));
        },
        transport =>
        {
            transport.Authentication(new BasicAuthentication("elastic", "elastic"));
        })
    .ReadFrom.Configuration(new ConfigurationBuilder()
        .AddJsonFile("appsettings.json")
        .AddJsonFile($"appsettings.{environment}.json", optional: true)
        .AddEnvironmentVariables()
        .Build())
    .CreateLogger();

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog();

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

builder.Services.AddAllElasticApm();

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

app.Run();
