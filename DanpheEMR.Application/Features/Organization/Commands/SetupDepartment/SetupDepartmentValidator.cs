using FluentValidation;

namespace DanpheEMR.Application.Features.Admin.Commands.SetupDepartment
{
    public class SetupDepartmentValidator : AbstractValidator<SetupDepartmentCommand>
    {
        public SetupDepartmentValidator()
        {
            RuleFor(x => x.DepartmentCode)
                .NotEmpty().WithMessage("Mã khoa/phòng không được để trống.")
                .MaximumLength(50).WithMessage("Mã khoa/phòng tối đa 50 ký tự.");

            RuleFor(x => x.DepartmentName)
                .NotEmpty().WithMessage("Tên khoa/phòng không được để trống.")
                .MaximumLength(150).WithMessage("Tên khoa/phòng tối đa 150 ký tự.");
        }
    }
}