using FluentValidation;

namespace DanpheEMR.Application.Features.Admin.Commands.AssignUserRole
{
    public class AssignUserRoleValidator : AbstractValidator<AssignUserRoleCommand>
    {
        public AssignUserRoleValidator()
        {
            RuleFor(x => x.UserCode).NotEmpty().WithMessage("Vui lòng chọn người dùng cần gán quyền.");
            RuleFor(x => x.RoleCode).NotEmpty().WithMessage("Vui lòng chọn vai trò (Role) muốn gán.");
        }
    }
}