using AppLS10.application.DTOs;
using AppLS10.application.Services;
using AppLS10.domain.Puertos;

namespace AppLS10.application.UseCases.Auth;

public class LoginUseCase
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ITokenService _tokenService;

    public LoginUseCase(IUnitOfWork unitOfWork, ITokenService tokenService)
    {
        _unitOfWork = unitOfWork;
        _tokenService = tokenService;
    }

    public async Task<AuthResponseDto?> ExecuteAsync(LoginDto dto)
    {
        var user = await _unitOfWork.Users.GetByUsernameAsync(dto.Username);
        if (user == null || user.PasswordHash != dto.Password)
            return null;

        var roles = user.UserRoles.Select(ur => ur.Role.RoleName).ToList();
        var token = _tokenService.GenerateToken(user.UserId, user.Username, roles);

        return new AuthResponseDto { Token = token, Username = user.Username, Roles = roles };
    }
}