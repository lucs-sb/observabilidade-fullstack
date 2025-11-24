using Auth.API.Models;
using Auth.Domain.DTOs;
using Auth.Domain.DTOs.Response;
using Auth.Domain.Intefaces;
using Mapster;
using Microsoft.AspNetCore.Mvc;

namespace Auth.API.Controllers;

[ApiController]
[Route("/api/auth")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost]
    public async Task<IActionResult> LoginAsync([FromBody] LoginModel loginModel)
    {
        LoginDTO loginDTO = loginModel.Adapt<LoginDTO>();

        LoginResponseDTO loginResponseDTO = await _authService.LoginAsync(loginDTO);

        return Ok(loginResponseDTO);
    }
}