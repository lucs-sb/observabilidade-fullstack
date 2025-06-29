namespace Gateway.API.Mappers;

public static class MappingConfigurations
{
    public static IServiceCollection RegisterMaps(this IServiceCollection services)
    {
        services.RegisterAuthMaps();
        services.RegisterDonorMaps();

        return services;
    }
}