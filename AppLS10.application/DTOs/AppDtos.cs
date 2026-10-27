using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using AppLS10.domain.Constants;

namespace AppLS10.application.DTOs;

// ─── Auth DTOs ───────────────────────────────────────────────────────────────

public class RegisterDto
{
    [Required]
    [MaxLength(100)]
    public string Username { get; set; } = null!;

    [Required]
    [MinLength(6)]
    public string Password { get; set; } = null!;

    [EmailAddress]
    [MaxLength(150)]
    public string? Email { get; set; }
}

public class LoginDto
{
    [Required]
    public string Username { get; set; } = null!;

    [Required]
    public string Password { get; set; } = null!;
}

public class AuthResponseDto
{
    public string Token    { get; set; } = null!;
    public string Username { get; set; } = null!;
    public List<string> Roles { get; set; } = [];
}

// ─── User / Role DTOs ────────────────────────────────────────────────────────

public class UserDto
{
    public Guid       UserId    { get; set; }
    public string     Username  { get; set; } = null!;
    public string?    Email     { get; set; }
    public DateTime?  CreatedAt { get; set; }
    public List<string> Roles   { get; set; } = [];
}

public class RoleDto
{
    public Guid   RoleId   { get; set; }
    public string RoleName { get; set; } = null!;
}

// ─── Ticket DTOs ─────────────────────────────────────────────────────────────

public class TicketDto
{
    public Guid      TicketId    { get; set; }
    public Guid      UserId      { get; set; }
    public string    Title       { get; set; } = null!;
    public string?   Description { get; set; }
    public string    Status      { get; set; } = null!;
    public DateTime? CreatedAt   { get; set; }
    public DateTime? ClosedAt    { get; set; }
}

public class CreateTicketDto
{
    [Required]
    [MaxLength(255)]
    public string Title { get; set; } = null!;

    public string? Description { get; set; }
}

/// <summary>
/// JsonStringEnumConverter permite recibir "abierto" / "en_proceso" / "cerrado" como string JSON.
/// </summary>
public class UpdateTicketStatusDto
{
    [Required]
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public TicketStatus Status { get; set; }
}

// ─── Response DTOs ───────────────────────────────────────────────────────────

public class ResponseDto
{
    public Guid      ResponseId  { get; set; }
    public Guid      TicketId    { get; set; }
    public Guid      ResponderId { get; set; }
    public string    Message     { get; set; } = null!;
    public DateTime? CreatedAt   { get; set; }
}

public class CreateResponseDto
{
    [Required]
    public string Message { get; set; } = null!;
}
