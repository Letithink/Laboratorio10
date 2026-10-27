using AppLS10.api.Extensions;
using AppLS10.application.DTOs;
using AppLS10.application.UseCases.Tickets;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AppLS10.api.Controllers;

[ApiController]
[Route("api/tickets")]
public class TicketController : ControllerBase
{
    private readonly CreateTicketUseCase _createTicketUseCase;
    private readonly GetTicketsUseCase _getTicketsUseCase;
    private readonly GetMyTicketsUseCase _getMyTicketsUseCase;
    private readonly GetTicketByIdUseCase _getTicketByIdUseCase;
    private readonly UpdateTicketStatusUseCase _updateTicketStatusUseCase;
    private readonly DeleteTicketUseCase _deleteTicketUseCase;

    public TicketController(
        CreateTicketUseCase createTicketUseCase,
        GetTicketsUseCase getTicketsUseCase,
        GetMyTicketsUseCase getMyTicketsUseCase,
        GetTicketByIdUseCase getTicketByIdUseCase,
        UpdateTicketStatusUseCase updateTicketStatusUseCase,
        DeleteTicketUseCase deleteTicketUseCase)
    {
        _createTicketUseCase = createTicketUseCase;
        _getTicketsUseCase = getTicketsUseCase;
        _getMyTicketsUseCase = getMyTicketsUseCase;
        _getTicketByIdUseCase = getTicketByIdUseCase;
        _updateTicketStatusUseCase = updateTicketStatusUseCase;
        _deleteTicketUseCase = deleteTicketUseCase;
    }

    [HttpPost]
    [Authorize(Policy = "AdminOrUser")]
    [ProducesResponseType(typeof(TicketDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> Create([FromBody] CreateTicketDto dto)
    {
        var userId = User.GetUserId();
        var ticket = await _createTicketUseCase.ExecuteAsync(userId, dto);
        return CreatedAtAction(nameof(GetById), new { id = ticket.TicketId }, ticket);
    }

    [HttpGet]
    [Authorize(Policy = "AdminOnly")]
    [ProducesResponseType(typeof(IEnumerable<TicketDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<IActionResult> GetAll([FromQuery] string? status = null)
    {
        var tickets = await _getTicketsUseCase.ExecuteAsync(status);
        return Ok(tickets);
    }

    [HttpGet("my-tickets")]
    [Authorize(Policy = "AdminOrUser")]
    [ProducesResponseType(typeof(IEnumerable<TicketDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> GetMyTickets()
    {
        var userId = User.GetUserId();
        var tickets = await _getMyTicketsUseCase.ExecuteAsync(userId);
        return Ok(tickets);
    }

    [HttpGet("{id:guid}")]
    [Authorize(Policy = "AdminOnly")]
    [ProducesResponseType(typeof(TicketDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById([FromRoute] Guid id)
    {
        var ticket = await _getTicketByIdUseCase.ExecuteAsync(id);
        if (ticket == null)
            return NotFound(new { message = $"Ticket con ID '{id}' no encontrado." });

        return Ok(ticket);
    }

    [HttpPut("{id:guid}/status")]
    [Authorize(Policy = "AdminOnly")]
    [ProducesResponseType(typeof(TicketDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateStatus([FromRoute] Guid id, [FromBody] UpdateTicketStatusDto dto)
    {
        var updated = await _updateTicketStatusUseCase.ExecuteAsync(id, dto);
        if (updated == null)
            return NotFound(new { message = $"Ticket con ID '{id}' no encontrado." });

        return Ok(updated);
    }

    [HttpDelete("{id:guid}")]
    [Authorize(Policy = "AdminOnly")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        var deleted = await _deleteTicketUseCase.ExecuteAsync(id);
        if (!deleted)
            return NotFound(new { message = $"Ticket con ID '{id}' no encontrado." });

        return NoContent();
    }
}