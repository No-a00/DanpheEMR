
namespace DanpheEMR.Application.Features.Appointment.Commands.BookAppointment
{
    public class BookAppointmentCommandHandler
    : BaseHandler<
        BookAppointmentCommand,
        BookAppointmentResponse>
    {
        public override async Task<BookAppointmentResponse> Handle(BookAppointmentCommand request, CancellationToken cancellationToken)
        {
            // Viết logic Đặt lịch khám của bạn ở đây

            return new BookAppointmentResponse();
        }
    }
}
