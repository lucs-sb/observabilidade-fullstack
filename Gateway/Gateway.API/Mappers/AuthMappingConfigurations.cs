using Gateway.API.Models.Auth;
using Gateway.Domain.DTOs;
using Gateway.Domain.DTOs.Response;
using Gateway.Infrastructure.Integrations.Message.Request.Auth;
using Gateway.Infrastructure.Integrations.Message.Response.Auth;
using Mapster;

namespace Gateway.API.Mappers;

public static class AuthMappingConfigurations
{
    public static void RegisterAuthMaps(this IServiceCollection services)
    {
        TypeAdapterConfig<LoginModel, LoginDTO>
            .NewConfig()
            .Map(dest => dest.Identifier, src => src.Identifier)
            .Map(dest => dest.Password, src => src.Password);

        TypeAdapterConfig<LoginDTO, LoginRequest>
            .NewConfig()
            .Map(dest => dest.Identifier, src => src.Identifier)
            .Map(dest => dest.Password, src => src.Password);

        TypeAdapterConfig<LoginResponse, LoginResponseDTO>
            .NewConfig()
            .Map(dest => dest.access_token, src => src.Token)
            .Map(dest => dest.expiration, src => src.Expiration);
    }
}