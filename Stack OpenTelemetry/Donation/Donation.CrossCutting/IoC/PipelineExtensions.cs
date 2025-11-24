using Donation.Application.Services;
using Donation.Domain.Intefaces;
using Donation.Domain.Intefaces.Repositories;
using Donation.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Donation.CrossCutting.IoC;

public static class PipelineExtensions
{
    public static void AddApplicationDI(this IServiceCollection services)
    {
        services.AddScoped<IDonationService, DonationService>();
    }

    public static void AddAInfrastructureDI(this IServiceCollection services)
    {
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
    }
}