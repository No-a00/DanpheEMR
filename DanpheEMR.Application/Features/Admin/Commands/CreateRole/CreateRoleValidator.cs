using FluentValidation;

namespace DanpheEMR.Application.Features.Admin.Commands.CreateRole
{
    public class CreateRoleValidator : AbstractValidator<CreateRoleCommand>
    {
        public CreateRoleValidator()
        {
            RuleFor(x => x.RoleName)
                .NotEmpty().WithMessage("Tên vai trò không được để trống.")
                .MaximumLength(50).WithMessage("Tên vai trò không được vượt quá 50 ký tự.");
        }
    }
}