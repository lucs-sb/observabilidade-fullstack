using Donor.API.Mappers;
using Donor.API.Middleware;
using Donor.CrossCutting.IoC;
using Donor.Infrastructure.Repositories.Context;
using Elastic.Apm.NetCoreAll;
using Elastic.Ingest.Elasticsearch.DataStreams;
using Elastic.Serilog.Sinks;
using Elastic.Transport;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using Serilog;
using System.Text.Json.Serialization;

var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production";

Log.Logger = new LoggerConfiguration()
    .Enrich.FromLogContext()
    .Enrich.WithMachineName()
    .Enrich.WithThreadId()
    .Enrich.WithProperty("Application", "donor-api")
    .WriteTo.Console()
    .WriteTo.Elasticsearch(
        new[] { new Uri("http://elasticsearch:9200") },
        opts =>
        {
            opts.DataStream = new DataStreamName("logs", "donor-api", environment.ToLower().Replace(".", "-"));
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

//Mapster
builder.Services.RegisterDonorMaps();

var app = builder.Build();

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
