using FluentValidation;

namespace DanpheEMR.Application.Features.EMR.Queries.GetPatientMedicalHistory
{
    public class GetPatientMedicalHistoryQueryValidator : AbstractValidator<GetPatientMedicalHistoryQuery>
    {
        public GetPatientMedicalHistoryQueryValidator()
        {
            RuleFor(x => x.PatientId)
                .NotEmpty().WithMessage("Mã bệnh nhân không được để trống.");
        }
    }
}