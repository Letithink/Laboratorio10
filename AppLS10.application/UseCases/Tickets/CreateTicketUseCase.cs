using AppLS10.application.DTOs;
using AppLS10.application.Mapping;
using AppLS10.domain.Constants;
using AppLS10.domain.Entities;
using AppLS10.domain.Puertos;

namespace AppLS10.application.UseCases.Tickets;

public class CreateTicketUseCase
{
    private readonly IUnitOfWork _unitOfWork;

    public CreateTicketUseCase(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<TicketDto> ExecuteAsync(Guid userId, CreateTicketDto dto)
    {
        var ticket = new Ticket
        {
            TicketId = Guid.NewGuid(),
            UserId = userId,
            Title = dto.Title,
            Description = dto.Description,
            Status = TicketStatus.abierto.ToString()
        };

        await _unitOfWork.Tickets.AddAsync(ticket);
        await _unitOfWork.SaveChangesAsync();

        return ticket.ToDto();
    }
}