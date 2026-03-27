using FluentValidation;

namespace DanpheEMR.Application.Features.BloodBank.Commands.IssueBlood
{
    public class IssueBloodValidator : AbstractValidator<IssueBloodCommand>
    {
        public IssueBloodValidator()
        {
            RuleFor(x => x.PatientId)
                .NotEmpty().WithMessage("Vui lòng chọn Bệnh nhân.");

            RuleFor(x => x.BloodGroupId)
                .NotEmpty().WithMessage("Vui lòng chọn Nhóm máu.");

            RuleFor(x => x.Quantity)
                .GreaterThan(0).WithMessage("Số lượng máu xuất phải lớn hơn 0.");
        }
    }
}