using Auth.Domain.DTOs.Response;

namespace Auth.Domain.Intefaces;

public interface ITokenService
{
    Task<LoginResponseDTO> GenerateToken(string id);
}