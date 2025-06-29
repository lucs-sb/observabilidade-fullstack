using Gateway.Application.Services;
using Gateway.Domain.Interfaces;
using Gateway.Domain.Interfaces.Http;
using Gateway.Domain.Interfaces.Integration;
using Gateway.Domain.Settings;
using Gateway.Infrastructure.Integrations.Client;
using Gateway.Infrastructure.Integrations.Service;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Gateway.CrossCutting.IoC;

public static class PipelineExtensions
{
    public static void AddApplicationDI(this IServiceCollection services)
    {
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IDonorService, DonorService>();
    }

    public static void AddAInfrastructureDI(this IServiceCollection services)
    {
        services.AddScoped<IAuthIntegrationService, AuthIntegrationService>();
        services.AddScoped<IDonorIntegrationService, DonorIntegrationService>();

        services.AddScoped<IMicroApiClient, MicroApiClient>();
    }

    public static void AddConfigurationDependencies(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<IntegrationSettings>(configuration.GetSection("IntegrationSettings"));
    }
}
