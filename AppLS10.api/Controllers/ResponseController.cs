using AppLS10.api.Extensions;
using AppLS10.application.DTOs;
using AppLS10.application.UseCases.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AppLS10.api.Controllers;

[ApiController]
[Route("api/tickets/{ticketId:guid}/responses")]
[Authorize(Policy = "AdminOrUser")]
public class ResponseController : ControllerBase
{
    private readonly AddResponseUseCase _addResponseUseCase;
    private readonly GetResponsesByTicketUseCase _getResponsesByTicketUseCase;

    public ResponseController(
        AddResponseUseCase addResponseUseCase,
        GetResponsesByTicketUseCase getResponsesByTicketUseCase)
    {
        _addResponseUseCase = addResponseUseCase;
        _getResponsesByTicketUseCase = getResponsesByTicketUseCase;
    }

    [HttpPost]
    [ProducesResponseType(typeof(ResponseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Create(Guid ticketId, [FromBody] CreateResponseDto dto)
    {
        var created = await _addResponseUseCase.ExecuteAsync(ticketId, User.GetUserId(), dto);
        if (created == null)
            return NotFound(new { message = $"Ticket con ID '{ticketId}' no encontrado." });

        return Ok(created);
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<ResponseDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetByTicket(Guid ticketId)
    {
        var responses = await _getResponsesByTicketUseCase.ExecuteAsync(ticketId);
        if (responses == null)
            return NotFound(new { message = $"Ticket con ID '{ticketId}' no encontrado." });

        return Ok(responses);
    }
}