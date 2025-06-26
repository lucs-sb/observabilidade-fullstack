using Gateway.Domain.DTOs;
using Gateway.Domain.DTOs.Response;
using Gateway.Domain.Interfaces;
using Gateway.Domain.Interfaces.Integration;

namespace Gateway.Application.Services;

public class AuthService : IAuthService
{
    private readonly IAuthIntegrationService _authIntegrationService;

    public AuthService(IAuthIntegrationService authIntegrationService)
    {
        _authIntegrationService = authIntegrationService;
    }

    public async Task<LoginResponseDTO> LoginAsync(LoginDTO loginDTO)
    {
        try
        {
            return await _authIntegrationService.LoginAsync(loginDTO);
        }
        catch (Exception)
        {
            throw;
        }
    }
}