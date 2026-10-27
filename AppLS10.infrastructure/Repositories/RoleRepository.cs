using AppLS10.domain.Entities;
using AppLS10.domain.Puertos;
using AppLS10.infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace AppLS10.infrastructure.Repositories;

public class RoleRepository : Repository<Role>, IRoleRepository
{
    public RoleRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<Role?> GetByNameAsync(string roleName) =>
        await _dbSet.FirstOrDefaultAsync(r => r.RoleName == roleName);
}