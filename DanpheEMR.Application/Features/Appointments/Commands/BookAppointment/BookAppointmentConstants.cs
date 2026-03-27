namespace DanpheEMR.Application.Features.Appointment.Commands.BookAppointment
{
    public static class BookAppointmentConstants
    {
        // Độ dài tối đa cho lý do khám
        public const int MaxReasonLength = 500;

        // Phải đặt lịch trước ít nhất bao nhiêu tiếng?
        public const int MinAdvanceBookingHours = 24;

        // Thời lượng mặc định của một ca khám (phút)
        public const int DefaultDurationMinutes = 30;
    }
}