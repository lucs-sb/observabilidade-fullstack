using Auth.Application.Services;
using Auth.Domain.Intefaces;
using Auth.Domain.Intefaces.Repositories;
using Auth.Domain.Settings;
using Auth.Infrastructure.Auth;
using Auth.Infrastructure.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Auth.CrossCutting.IoC;

public static class PipelineExtensions
{
    public static void AddApplicationDI(this IServiceCollection services)
    {
        services.AddScoped<IAuthService, AuthService>();
    }

    public static void AddAInfrastructureDI(this IServiceCollection services)
    {
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

        services.AddScoped<ITokenService, TokenService>();

        services.AddScoped(typeof(IPasswordHasher<>), typeof(PasswordHasher<>));
    }

    public static void AddSettings(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<AuthSettings>(configuration.GetSection("AuthSettings"));
    }
}