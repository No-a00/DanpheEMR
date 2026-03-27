using FluentValidation;

namespace DanpheEMR.Application.Features.Appointments.Queries.GetPatientAppointments
{
    public class GetPatientAppointmentsQueryValidator : AbstractValidator<GetPatientAppointmentsQuery>
    {
        public GetPatientAppointmentsQueryValidator()
        {
            RuleFor(x => x.PatientId)
                .NotEmpty().WithMessage("Mã bệnh nhân không được để trống.");
        }
    }
}