using Auth.Application.Resources;
using Auth.Domain.DTOs;
using Auth.Domain.DTOs.Response;
using Auth.Domain.Entities;
using Auth.Domain.Intefaces;
using Auth.Domain.Intefaces.Repositories;
using Microsoft.AspNetCore.Identity;

namespace Auth.Application.Services;

public class AuthService : IAuthService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IPasswordHasher<User> _passwordHasher;
    private readonly ITokenService _tokenService;

    public AuthService(IUnitOfWork unitOfWork, IPasswordHasher<User> passwordHasher, ITokenService tokenService)
    {
        _unitOfWork = unitOfWork;
        _passwordHasher = passwordHasher;
        _tokenService = tokenService;
    }

    public async Task<LoginResponseDTO> LoginAsync(LoginDTO loginDTO)
    {
        try
        {
            User user = await _unitOfWork.Repository<User>().GetByIdentifierAsync(loginDTO.Identifier) ?? throw new InvalidOperationException(string.Format(BusinessMessage.Unauthorized_Warning));

            if (_passwordHasher.VerifyHashedPassword(user, user.Password!, loginDTO.Password) is PasswordVerificationResult.Failed)
                throw new InvalidOperationException(string.Format(BusinessMessage.Unauthorized_Warning));

            return await _tokenService.GenerateToken(user.Id.ToString());
        }
        catch (InvalidOperationException ex)
        {
            throw new UnauthorizedAccessException(ex.Message);
        }
        catch (Exception)
        {
            throw new UnauthorizedAccessException();
        }
    }
}