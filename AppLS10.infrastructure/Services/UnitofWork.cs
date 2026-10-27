using AppLS10.domain.Puertos;
using AppLS10.infrastructure.Persistence.Context;
using AppLS10.infrastructure.Repositories;

namespace AppLS10.infrastructure.Services;

public class UnitOfWork : IUnitOfWork
{
    private readonly AppDbContext _context;
    private IUserRepository? _users;
    private IRoleRepository? _roles;
    private ITicketRepository? _tickets;
    private IResponseRepository? _responses;

    public UnitOfWork(AppDbContext context)
    {
        _context = context;
    }

    public IUserRepository Users => _users ??= new UserRepository(_context);

    public IRoleRepository Roles => _roles ??= new RoleRepository(_context);

    public ITicketRepository Tickets => _tickets ??= new TicketRepository(_context);

    public IResponseRepository Responses => _responses ??= new ResponseRepository(_context);

    public async Task<int> SaveChangesAsync() => await _context.SaveChangesAsync();

    public void Dispose() => _context.Dispose();
}