using AppLS10.application.DTOs;
using AppLS10.application.Mapping;
using AppLS10.domain.Puertos;

namespace AppLS10.application.UseCases.Users;

public class GetUserByIdUseCase
{
    private readonly IUnitOfWork _unitOfWork;

    public GetUserByIdUseCase(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<UserDto?> ExecuteAsync(Guid id)
    {
        var user = await _unitOfWork.Users.GetByIdWithRolesAsync(id);
        return user?.ToDto();
    }
}