using FluentValidation;


namespace DanpheEMR.Application.Features.Appointment.Commands.BookAppointment
{
    public class BookAppointmentCommandValidator
    : BaseValidator<BookAppointmentCommand>
    {
        public BookAppointmentCommandValidator()
        {
            RuleFor(x => x.PatientId)
                .NotEmpty();

            RuleFor(x => x.DoctorId)
                .NotEmpty();

            RuleFor(x => x.AppointmentDate)
                .GreaterThan(DateTime.Now);
        }
    }
}
