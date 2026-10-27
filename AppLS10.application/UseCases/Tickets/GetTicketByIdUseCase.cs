using AppLS10.application.DTOs;
using AppLS10.application.Mapping;
using AppLS10.domain.Puertos;

namespace AppLS10.application.UseCases.Tickets;

public class GetTicketByIdUseCase
{
    private readonly IUnitOfWork _unitOfWork;

    public GetTicketByIdUseCase(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<TicketDto?> ExecuteAsync(Guid id)
    {
        var ticket = await _unitOfWork.Tickets.GetByIdAsync(id);
        return ticket?.ToDto();
    }
}