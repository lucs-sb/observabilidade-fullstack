using Donor.Application.Services;
using Donor.Domain.Intefaces;
using Donor.Domain.Intefaces.Repositories;
using Donor.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Donor.CrossCutting.IoC;

public static class PipelineExtensions
{
    public static void AddApplicationDI(this IServiceCollection services)
    {
        services.AddScoped<IDonorService, DonorService>();
    }

    public static void AddAInfrastructureDI(this IServiceCollection services)
    {
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
    }
}