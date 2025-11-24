using Auth.Domain.DTOs;
using Auth.Domain.DTOs.Response;

namespace Auth.Domain.Intefaces;

public interface IAuthService
{
    Task<LoginResponseDTO> LoginAsync(LoginDTO loginDTO);
}