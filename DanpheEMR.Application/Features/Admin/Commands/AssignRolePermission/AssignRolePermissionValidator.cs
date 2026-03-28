using FluentValidation;

namespace DanpheEMR.Application.Features.Admin.Commands.AssignRolePermission
{
    public class AssignRolePermissionValidator : AbstractValidator<AssignRolePermissionCommand>
    {
        public AssignRolePermissionValidator()
        {
            RuleFor(x => x.RoleId).NotEmpty().WithMessage("Vui lòng chọn Vai trò (Role).");
            RuleFor(x => x.PermissionId).NotEmpty().WithMessage("Vui lòng chọn Quyền (Permission) cần gán.");
        }
    }
}