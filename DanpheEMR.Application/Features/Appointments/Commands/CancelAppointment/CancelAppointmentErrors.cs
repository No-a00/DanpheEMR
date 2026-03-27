using Application.Common;

namespace DanpheEMR.Application.Features.Appointments.Commands.CancelAppointment
{
    public static class CancelAppointmentErrors
    {
        public static readonly Error NotFound = new Error(
            "CancelAppointment.NotFound",
            "Không tìm thấy lịch hẹn này trong hệ thống.");

        public static readonly Error AlreadyCanceled = new Error(
            "CancelAppointment.AlreadyCanceled",
            "Lịch hẹn này đã bị hủy trước đó.");

        public static readonly Error DatabaseError = new Error(
            "CancelAppointment.DatabaseError",
            "Đã xảy ra lỗi khi lưu thao tác hủy vào cơ sở dữ liệu.");
    }
}