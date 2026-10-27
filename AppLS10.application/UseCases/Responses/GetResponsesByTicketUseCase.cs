using AppLS10.application.DTOs;
using AppLS10.application.Mapping;
using AppLS10.domain.Puertos;

namespace AppLS10.application.UseCases.Responses;

public class GetResponsesByTicketUseCase
{
    private readonly IUnitOfWork _unitOfWork;

    public GetResponsesByTicketUseCase(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<ResponseDto>?> ExecuteAsync(Guid ticketId)
    {
        var ticket = await _unitOfWork.Tickets.GetByIdAsync(ticketId);
        if (ticket == null)
            return null;

        var responses = await _unitOfWork.Responses.GetByTicketIdAsync(ticketId);
        return responses.Select(r => r.ToDto());
    }
}