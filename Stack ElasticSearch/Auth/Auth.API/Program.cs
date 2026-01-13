using Auth.API.Mappers;
using Auth.API.Middleware;
using Auth.CrossCutting.IoC;
using Auth.Domain.Entities;
using Auth.Infrastructure.Repositories.Context;
using Elastic.Apm.NetCoreAll;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Npgsql;
using System.Text.Json.Serialization;
using Serilog;
using Serilog.Sinks.Elasticsearch;

var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production";

Log.Logger = new LoggerConfiguration()
    .Enrich.FromLogContext()
    .Enrich.WithMachineName()
    .Enrich.WithThreadId()
    .Enrich.WithProperty("Application", "auth-api")
    .WriteTo.Console()
    .WriteTo.Elasticsearch(new ElasticsearchSinkOptions(new Uri("http://elasticsearch:9200"))
    {
        AutoRegisterTemplate = true,
        IndexFormat = $"auth-api-logs-{environment.ToLower().Replace(".", "-")}-{DateTime.UtcNow:yyyy-MM}",
        ModifyConnectionSettings = conn =>
        conn.BasicAuthentication("elastic", "elastic")
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
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("Database"));
});

builder.Services.AddAllElasticApm();

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

app.Run();
