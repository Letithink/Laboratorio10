using AppLS10.domain.Entities;

namespace AppLS10.domain.Puertos;

public interface IRoleRepository : IRepository<Role>
{
    Task<Role?> GetByNameAsync(string roleName);
}