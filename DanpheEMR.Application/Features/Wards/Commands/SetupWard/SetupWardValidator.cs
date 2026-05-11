using FluentValidation;

namespace DanpheEMR.Application.Features.Inpatient.Commands.SetupWard
{
    public class SetupWardValidator : AbstractValidator<SetupWardCommand>
    {
        public SetupWardValidator()
        {
            RuleFor(x => x.WardName).NotEmpty().WithMessage("Tên buồng bệnh không được để trống.");
            RuleFor(x => x.WardCode).NotEmpty().WithMessage("Mã buồng bệnh không được để trống.");
            RuleFor(x => x.DepartmentCode).NotEmpty().WithMessage("Vui lòng chọn Khoa trực thuộc.");
        }
    }
}