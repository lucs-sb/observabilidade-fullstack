using Gateway.Domain.DTOs;
using Gateway.Domain.DTOs.Response;

namespace Gateway.Domain.Interfaces;

public interface IAuthService
{
    Task<LoginResponseDTO> LoginAsync(LoginDTO loginDTO);
}