using AppLS10.domain.Entities;

namespace AppLS10.domain.Puertos;

public interface ITicketRepository : IRepository<Ticket>
{
    Task<IEnumerable<Ticket>> GetByUserIdAsync(Guid userId);
}