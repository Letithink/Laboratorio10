using AppLS10.domain.Entities;

namespace AppLS10.domain.Puertos;

public interface IResponseRepository : IRepository<Response>
{
    Task<IEnumerable<Response>> GetByTicketIdAsync(Guid ticketId);
}