using AppLS10.domain.Entities;
using AppLS10.domain.Puertos;
using AppLS10.infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace AppLS10.infrastructure.Repositories;

public class ResponseRepository : Repository<Response>, IResponseRepository
{
    public ResponseRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Response>> GetByTicketIdAsync(Guid ticketId) =>
        await _dbSet.Where(r => r.TicketId == ticketId).OrderBy(r => r.CreatedAt).ToListAsync();
}