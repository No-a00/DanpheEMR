
namespace DanpheEMR.Application.Features.Appointment.Commands.BookAppointment
{
    public class BookAppointmentCommand
    : BaseCommand<BookAppointmentResponse>
    {
        public Guid PatientId { get; set; }

        public Guid DoctorId { get; set; }

        public DateTime AppointmentDate { get; set; }

        public string Reason { get; set; }
    }
}
