using Auth.API.Models;
using Auth.Domain.DTOs;
using Mapster;

namespace Auth.API.Mappers;

public static class AuthMappingConfigurations
{
    public static void RegisterAuthMaps(this IServiceCollection services)
    {
        TypeAdapterConfig<LoginModel, LoginDTO>
            .NewConfig()
            .Map(dest => dest.Identifier, src => src.Identifier)
            .Map(dest => dest.Password, src => src.Password);
    }
}