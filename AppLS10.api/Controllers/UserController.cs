using AppLS10.application.DTOs;
using AppLS10.application.UseCases.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AppLS10.api.Controllers;

[ApiController]
[Route("api/users")]
[Authorize(Policy = "AdminOnly")]
public class UserController : ControllerBase
{
    private readonly GetUsersUseCase _getUsersUseCase;
    private readonly GetUserByIdUseCase _getUserByIdUseCase;

    public UserController(
        GetUsersUseCase getUsersUseCase,
        GetUserByIdUseCase getUserByIdUseCase)
    {
        _getUsersUseCase = getUsersUseCase;
        _getUserByIdUseCase = getUserByIdUseCase;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<UserDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<IActionResult> GetAll()
    {
        var users = await _getUsersUseCase.ExecuteAsync();
        return Ok(users);
    }

    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(UserDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById([FromRoute] Guid id)
    {
        var user = await _getUserByIdUseCase.ExecuteAsync(id);
        if (user == null)
            return NotFound(new { message = $"Usuario con ID '{id}' no encontrado." });

        return Ok(user);
    }
}