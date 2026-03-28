using FluentValidation;

namespace DanpheEMR.Application.Features.Patients.Commands.TransferPatient
{
    public class TransferPatientValidator : AbstractValidator<TransferPatientCommand>
    {
        public TransferPatientValidator()
        {
            RuleFor(x => x.AdmissionId).NotEmpty();
            RuleFor(x => x.FromDeptId).NotEmpty();
            RuleFor(x => x.ToDeptId).NotEmpty();
            RuleFor(x => x.Reason).NotEmpty().WithMessage("Vui lòng nhập lý do chuyển khoa.");

            
            RuleFor(x => x).Must(x => x.FromDeptId != x.ToDeptId)
                .WithMessage("Khoa chuyển đến phải khác khoa hiện tại.");
        }
    }
}