using AppLS10.application.DTOs;
using AppLS10.application.Mapping;
using AppLS10.domain.Constants;
using AppLS10.domain.Puertos;

namespace AppLS10.application.UseCases.Tickets;

public class UpdateTicketStatusUseCase
{
    private readonly IUnitOfWork _unitOfWork;

    public UpdateTicketStatusUseCase(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<TicketDto?> ExecuteAsync(Guid id, UpdateTicketStatusDto dto)
    {
        var ticket = await _unitOfWork.Tickets.GetByIdAsync(id);
        if (ticket == null)
            return null;

        ticket.Status = dto.Status.ToString();
        ticket.ClosedAt = dto.Status == TicketStatus.cerrado
            ? DateTime.SpecifyKind(DateTime.UtcNow, DateTimeKind.Unspecified)
            : null;

        _unitOfWork.Tickets.Update(ticket);
        await _unitOfWork.SaveChangesAsync();

        return ticket.ToDto();
    }
}
