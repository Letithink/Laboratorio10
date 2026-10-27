using AppLS10.application.DTOs;
using AppLS10.application.Mapping;
using AppLS10.domain.Entities;
using AppLS10.domain.Puertos;

namespace AppLS10.application.UseCases.Responses;

public class AddResponseUseCase
{
    private readonly IUnitOfWork _unitOfWork;

    public AddResponseUseCase(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<ResponseDto?> ExecuteAsync(Guid ticketId, Guid responderId, CreateResponseDto dto)
    {
        var ticket = await _unitOfWork.Tickets.GetByIdAsync(ticketId);
        if (ticket == null)
            return null;

        var response = new Response
        {
            ResponseId = Guid.NewGuid(),
            TicketId = ticketId,
            ResponderId = responderId,
            Message = dto.Message
        };

        await _unitOfWork.Responses.AddAsync(response);
        await _unitOfWork.SaveChangesAsync();

        return response.ToDto();
    }
}