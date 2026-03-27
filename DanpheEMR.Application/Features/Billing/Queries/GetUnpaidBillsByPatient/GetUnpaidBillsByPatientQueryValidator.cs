using FluentValidation;

namespace DanpheEMR.Application.Features.Billing.Queries.GetUnpaidBillsByPatient
{
    public class GetUnpaidBillsByPatientQueryValidator : AbstractValidator<GetUnpaidBillsByPatientQuery>
    {
        public GetUnpaidBillsByPatientQueryValidator()
        {
            RuleFor(x => x.PatientId)
                .NotEmpty().WithMessage("Mã bệnh nhân không được để trống.");
        }
    }
}