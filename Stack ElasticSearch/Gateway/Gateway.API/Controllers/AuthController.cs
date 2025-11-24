using Gateway.API.Models.Auth;
using Gateway.Domain.DTOs;
using Gateway.Domain.DTOs.Response;
using Gateway.Domain.Interfaces;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Gateway.API.Controllers;

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
    [AllowAnonymous]
    public async Task<IActionResult> LoginAsync([FromBody] LoginModel loginModel)
    {
        LoginDTO loginDTO = loginModel.Adapt<LoginDTO>();

        LoginResponseDTO loginResponseDTO = await _authService.LoginAsync(loginDTO);

        return Ok(loginResponseDTO);
    }
}