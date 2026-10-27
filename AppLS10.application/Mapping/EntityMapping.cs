using AppLS10.application.DTOs;
using AppLS10.domain.Entities;

namespace AppLS10.application.Mapping;

public static class EntityMappings
{
    public static UserDto ToDto(this User user) => new()
    {
        UserId = user.UserId,
        Username = user.Username,
        Email = user.Email,
        CreatedAt = user.CreatedAt,
        Roles = user.UserRoles.Select(ur => ur.Role.RoleName).ToList()
    };

    public static RoleDto ToDto(this Role role) => new()
    {
        RoleId = role.RoleId,
        RoleName = role.RoleName
    };

    public static TicketDto ToDto(this Ticket ticket) => new()
    {
        TicketId = ticket.TicketId,
        UserId = ticket.UserId,
        Title = ticket.Title,
        Description = ticket.Description,
        Status = ticket.Status,
        CreatedAt = ticket.CreatedAt,
        ClosedAt = ticket.ClosedAt
    };

    public static ResponseDto ToDto(this Response response) => new()
    {
        ResponseId = response.ResponseId,
        TicketId = response.TicketId,
        ResponderId = response.ResponderId,
        Message = response.Message,
        CreatedAt = response.CreatedAt
    };
}