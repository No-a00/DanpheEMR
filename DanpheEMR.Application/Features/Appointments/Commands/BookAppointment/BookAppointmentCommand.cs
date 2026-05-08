using MediatR;

namespace DanpheEMR.Application.Features.Appointments.Commands.BookAppointment
{

    public class BookAppointmentCommand : IRequest<Result<BookAppointmentResponse>>
    {
        public string PatientCode { get; set; }
        public string DocTorCode { get; set; }
        public DateTime AppointmentDate { get; set; }
        public string Reason { get; set; }
    }
}