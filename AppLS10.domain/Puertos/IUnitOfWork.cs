namespace AppLS10.domain.Puertos;

public interface IUnitOfWork : IDisposable
{
    IUserRepository Users { get; }
    IRoleRepository Roles { get; }
    ITicketRepository Tickets { get; }
    IResponseRepository Responses { get; }

    Task<int> SaveChangesAsync();
}