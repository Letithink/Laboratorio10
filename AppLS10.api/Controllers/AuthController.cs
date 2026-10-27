using AppLS10.api.Extensions;
using AppLS10.application.DTOs;
using AppLS10.application.UseCases.Auth;
using AppLS10.application.UseCases.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AppLS10.api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly RegisterUseCase _registerUseCase;
    private readonly LoginUseCase _loginUseCase;
    private readonly GetUserByIdUseCase _getUserByIdUseCase;

    public AuthController(
        RegisterUseCase registerUseCase,
        LoginUseCase loginUseCase,
        GetUserByIdUseCase getUserByIdUseCase)
    {
        _registerUseCase = registerUseCase;
        _loginUseCase = loginUseCase;
        _getUserByIdUseCase = getUserByIdUseCase;
    }

    [HttpPost("register")]
    [AllowAnonymous]
    [ProducesResponseType(typeof(UserDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<IActionResult> Register([FromBody] RegisterDto dto)
    {
        var result = await _registerUseCase.ExecuteAsync(dto);
        if (result == null)
            return Conflict(new { message = "El nombre de usuario o correo ya está en uso." });

        return Ok(result);
    }

    [HttpPost("login")]
    [AllowAnonymous]
    [ProducesResponseType(typeof(AuthResponseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> Login([FromBody] LoginDto dto)
    {
        var result = await _loginUseCase.ExecuteAsync(dto);
        if (result == null)
            return Unauthorized(new { message = "Credenciales inválidas." });

        return Ok(result);
    }

    [HttpGet("me")]
    [Authorize]
    [ProducesResponseType(typeof(UserDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Me()
    {
        var userId = User.GetUserId();
        var user = await _getUserByIdUseCase.ExecuteAsync(userId);
        if (user == null)
            return NotFound(new { message = "Usuario no encontrado." });

        return Ok(user);
    }
}