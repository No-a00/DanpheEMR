using MediatR;

namespace DanpheEMR.Application.Features.Appointments.Commands.BookAppointment
{

    public class BookAppointmentCommand : IRequest<Result<BookAppointmentResponse>>
    {
        public Guid PatientId { get; set; }
        public Guid DoctorId { get; set; }
        public DateTime AppointmentDate { get; set; }
        public string Reason { get; set; }
    }
}