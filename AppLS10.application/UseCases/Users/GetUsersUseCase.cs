using AppLS10.application.DTOs;
using AppLS10.application.Mapping;
using AppLS10.domain.Puertos;

namespace AppLS10.application.UseCases.Users;

public class GetUsersUseCase
{
    private readonly IUnitOfWork _unitOfWork;

    public GetUsersUseCase(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<UserDto>> ExecuteAsync()
    {
        var users = await _unitOfWork.Users.GetAllWithRolesAsync();
        return users.Select(u => u.ToDto());
    }
}