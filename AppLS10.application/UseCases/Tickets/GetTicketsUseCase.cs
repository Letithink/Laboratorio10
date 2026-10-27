using AppLS10.application.DTOs;
using AppLS10.application.Mapping;
using AppLS10.domain.Puertos;

namespace AppLS10.application.UseCases.Tickets;

public class GetTicketsUseCase
{
    private readonly IUnitOfWork _unitOfWork;

    public GetTicketsUseCase(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<TicketDto>> ExecuteAsync(string? status = null)
    {
        var tickets = await _unitOfWork.Tickets.GetAllAsync();
        if (!string.IsNullOrWhiteSpace(status))
            tickets = tickets.Where(t => t.Status.Equals(status, StringComparison.OrdinalIgnoreCase));
        return tickets.Select(t => t.ToDto());
    }
}