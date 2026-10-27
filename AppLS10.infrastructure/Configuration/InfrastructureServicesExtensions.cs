using AppLS10.application.Services;
using AppLS10.application.UseCases.Auth;
using AppLS10.application.UseCases.Responses;
using AppLS10.application.UseCases.Roles;
using AppLS10.application.UseCases.Tickets;
using AppLS10.application.UseCases.Users;
using AppLS10.domain.Puertos;
using AppLS10.infrastructure.Persistence.Context;
using AppLS10.infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AppLS10.infrastructure.Configuration;

public static class InfrastructureServicesExtensions
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<AppDbContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));

        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<ITokenService, TokenService>();

        services.AddScoped<RegisterUseCase>();
        services.AddScoped<LoginUseCase>();
        services.AddScoped<GetUsersUseCase>();
        services.AddScoped<GetUserByIdUseCase>();
        services.AddScoped<GetRolesUseCase>();
        services.AddScoped<CreateTicketUseCase>();
        services.AddScoped<GetTicketsUseCase>();
        services.AddScoped<GetMyTicketsUseCase>();
        services.AddScoped<GetTicketByIdUseCase>();
        services.AddScoped<UpdateTicketStatusUseCase>();
        services.AddScoped<DeleteTicketUseCase>();
        services.AddScoped<AddResponseUseCase>();
        services.AddScoped<GetResponsesByTicketUseCase>();

        return services;
    }
}