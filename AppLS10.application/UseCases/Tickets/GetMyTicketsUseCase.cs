using AppLS10.application.DTOs;
using AppLS10.application.Mapping;
using AppLS10.domain.Puertos;

namespace AppLS10.application.UseCases.Tickets;

public class GetMyTicketsUseCase
{
    private readonly IUnitOfWork _unitOfWork;

    public GetMyTicketsUseCase(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<TicketDto>> ExecuteAsync(Guid userId)
    {
        var tickets = await _unitOfWork.Tickets.GetByUserIdAsync(userId);
        return tickets.Select(t => t.ToDto());
    }
}