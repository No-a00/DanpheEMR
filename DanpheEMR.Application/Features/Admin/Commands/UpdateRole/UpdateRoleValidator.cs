using FluentValidation;

namespace DanpheEMR.Application.Features.Admin.Commands.UpdateRole
{
    public class UpdateRoleValidator : AbstractValidator<UpdateRoleCommand>
    {
        public UpdateRoleValidator()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage("ID Vai trò không được để trống.");
            RuleFor(x => x.RoleName)
                .NotEmpty().WithMessage("Tên Vai trò không được để trống.")
                .MaximumLength(50).WithMessage("Tên Vai trò tối đa 50 ký tự.");
        }
    }
}