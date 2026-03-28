using FluentValidation;

namespace DanpheEMR.Application.Features.Patients.Commands.DischargePatient
{
    public class DischargePatientValidator : AbstractValidator<DischargePatientCommand>
    {
        public DischargePatientValidator()
        {
            RuleFor(x => x.AdmissionId).NotEmpty();
            RuleFor(x => x.DischargeCondition).NotEmpty().WithMessage("Vui lòng nhập tình trạng khi ra viện.");
        }
    }
}