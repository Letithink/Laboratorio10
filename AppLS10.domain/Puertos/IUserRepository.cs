using AppLS10.domain.Entities;

namespace AppLS10.domain.Puertos;

public interface IUserRepository : IRepository<User>
{
    Task<User?> GetByUsernameAsync(string username);
    Task<User?> GetByIdWithRolesAsync(Guid id);
    Task<IEnumerable<User>> GetAllWithRolesAsync();
}