using Gateway.Domain.DTOs;
using Gateway.Domain.DTOs.Response;

namespace Gateway.Domain.Interfaces.Integration;

public interface IAuthIntegrationService
{
    Task<LoginResponseDTO> LoginAsync(LoginDTO loginDTO);
}
