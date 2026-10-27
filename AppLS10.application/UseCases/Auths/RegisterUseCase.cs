using AppLS10.application.DTOs;
using AppLS10.application.Mapping;
using AppLS10.domain.Constants;
using AppLS10.domain.Entities;
using AppLS10.domain.Puertos;

namespace AppLS10.application.UseCases.Auth;

public class RegisterUseCase
{
    private readonly IUnitOfWork _unitOfWork;

    public RegisterUseCase(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    
    public async Task<UserDto?> ExecuteAsync(RegisterDto dto)
    {
        var users = (await _unitOfWork.Users.GetAllAsync()).ToList();

        if (users.Any(u => u.Username == dto.Username || (dto.Email != null && u.Email == dto.Email)))
            return null;

        var roleName = (users.Count == 0 ? RoleNames.Admin : RoleNames.User).ToString();
        var role = await _unitOfWork.Roles.GetByNameAsync(roleName);
        if (role == null)
        {
            role = new Role { RoleId = Guid.NewGuid(), RoleName = roleName };
            await _unitOfWork.Roles.AddAsync(role);
        }

        var user = new User
        {
            UserId = Guid.NewGuid(),
            Username = dto.Username,
            PasswordHash = dto.Password,
            Email = dto.Email
        };
        user.UserRoles.Add(new UserRole { Role = role });

        await _unitOfWork.Users.AddAsync(user);
        await _unitOfWork.SaveChangesAsync();

        return user.ToDto();
    }
}