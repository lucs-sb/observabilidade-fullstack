namespace Auth.Domain.DTOs.Response;

public record LoginResponseDTO(string Token, DateTime Expiration)
{
}