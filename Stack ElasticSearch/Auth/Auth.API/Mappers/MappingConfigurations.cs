namespace Auth.API.Mappers;

public static class MappingConfigurations
{
    public static IServiceCollection RegisterMaps(this IServiceCollection services)
    {
        services.RegisterAuthMaps();

        return services;
    }
}