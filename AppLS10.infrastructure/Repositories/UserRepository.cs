using AppLS10.domain.Entities;
using AppLS10.domain.Puertos;
using AppLS10.infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace AppLS10.infrastructure.Repositories;

public class UserRepository : Repository<User>, IUserRepository
{
    public UserRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<User?> GetByUsernameAsync(string username) =>
        await _dbSet
            .Include(u => u.UserRoles).ThenInclude(ur => ur.Role)
            .FirstOrDefaultAsync(u => u.Username == username);

    public async Task<User?> GetByIdWithRolesAsync(Guid id) =>
        await _dbSet
            .Include(u => u.UserRoles).ThenInclude(ur => ur.Role)
            .FirstOrDefaultAsync(u => u.UserId == id);

    public async Task<IEnumerable<User>> GetAllWithRolesAsync() =>
        await _dbSet
            .Include(u => u.UserRoles).ThenInclude(ur => ur.Role)
            .ToListAsync();
}