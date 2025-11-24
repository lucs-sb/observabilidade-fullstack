using Auth.API.Mappers;
using Auth.API.Middleware;
using Auth.CrossCutting.IoC;
using Auth.Domain.Entities;
using Auth.Infrastructure.Repositories.Context;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using OpenTelemetry.Exporter;
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
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("Database"));
});

builder.Services.AddOpenTelemetry()
    .ConfigureResource(res => res.AddService(builder.Environment.ApplicationName)) 
    .WithMetrics(metricsBuilder => {
        metricsBuilder
          .AddAspNetCoreInstrumentation()
          .AddHttpClientInstrumentation()
          .AddRuntimeInstrumentation()     
          .AddProcessInstrumentation()    
          .AddPrometheusExporter();
    })
    .WithTracing(tracingBuilder => {
        tracingBuilder
            .AddAspNetCoreInstrumentation()
            .AddHttpClientInstrumentation()
            .AddEntityFrameworkCoreInstrumentation() 
            .AddNpgsql() 
            .AddOtlpExporter(opt => opt.Endpoint = new Uri("http://otel-collector:4317")); 
    });

builder.Logging.ClearProviders();
builder.Logging.AddOpenTelemetry(options =>
{
    options.SetResourceBuilder(ResourceBuilder.CreateDefault().AddService(builder.Environment.ApplicationName));
    options.IncludeFormattedMessage = true;
    options.ParseStateValues = true;
    options.AddOtlpExporter(opt =>
    {
        opt.Endpoint = new Uri("http://otel-collector:4317");
    });
});

// Dependences Injections
builder.Services.AddApplicationDI();
builder.Services.AddAInfrastructureDI();
builder.Services.AddSettings(builder.Configuration);

//Mapster
builder.Services.RegisterMaps();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.EnsureCreated();

    if (!db.User.Any(d => d.Identifier == "lucas"))
    {
        db.User.Add(new User
        {
            Id = Guid.NewGuid(),
            Identifier = "lucas",
            Password = "AQAAAAIAAYagAAAAEOpeNiTeQJSbtTSL7MOlFOdo8isvYvJvqqYRM8flRzswiM1xp1+5HCwvhd46oawSiQ=="
        });
        db.SaveChanges();
    }
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ExceptionHandlingMiddleware>();

app.UseHttpsRedirection();

app.MapControllers();

app.MapPrometheusScrapingEndpoint();

app.Run();
