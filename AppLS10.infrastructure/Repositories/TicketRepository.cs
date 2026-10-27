using AppLS10.domain.Entities;
using AppLS10.domain.Puertos;
using AppLS10.infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace AppLS10.infrastructure.Repositories;

public class TicketRepository : Repository<Ticket>, ITicketRepository
{
    public TicketRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Ticket>> GetByUserIdAsync(Guid userId) =>
        await _dbSet.Where(t => t.UserId == userId).ToListAsync();
}