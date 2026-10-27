using AppLS10.application.DTOs;
using AppLS10.application.Mapping;
using AppLS10.domain.Puertos;

namespace AppLS10.application.UseCases.Roles;

public class GetRolesUseCase
{
    private readonly IUnitOfWork _unitOfWork;

    public GetRolesUseCase(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<RoleDto>> ExecuteAsync()
    {
        var roles = await _unitOfWork.Roles.GetAllAsync();
        return roles.Select(r => r.ToDto());
    }
}