using AppLS10.application.DTOs;
using AppLS10.application.UseCases.Roles;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AppLS10.api.Controllers;

[ApiController]
[Route("api/roles")]
[Authorize(Policy = "AdminOnly")]
public class RoleController : ControllerBase
{
    private readonly GetRolesUseCase _getRolesUseCase;

    public RoleController(GetRolesUseCase getRolesUseCase)
    {
        _getRolesUseCase = getRolesUseCase;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<RoleDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<IActionResult> GetAll()
    {
        var roles = await _getRolesUseCase.ExecuteAsync();
        return Ok(roles);
    }
}