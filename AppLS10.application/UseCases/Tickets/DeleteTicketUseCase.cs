using AppLS10.domain.Puertos;

namespace AppLS10.application.UseCases.Tickets;

public class DeleteTicketUseCase
{
    private readonly IUnitOfWork _unitOfWork;

    public DeleteTicketUseCase(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<bool> ExecuteAsync(Guid id)
    {
        var ticket = await _unitOfWork.Tickets.GetByIdAsync(id);
        if (ticket == null)
            return false;

        _unitOfWork.Tickets.Remove(ticket);
        await _unitOfWork.SaveChangesAsync();
        return true;
    }
}