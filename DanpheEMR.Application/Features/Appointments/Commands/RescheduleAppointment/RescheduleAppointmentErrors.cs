using Application.Common;

namespace DanpheEMR.Application.Features.Appointments.Commands.RescheduleAppointment
{
    public static class RescheduleAppointmentErrors
    {
        public static readonly Error NotFound = new Error(
            "RescheduleAppointment.NotFound",
            "Không tìm thấy lịch hẹn này trong hệ thống.");

        public static readonly Error InvalidStatus = new Error(
            "RescheduleAppointment.InvalidStatus",
            "Không thể dời lịch hẹn đã bị hủy hoặc đã hoàn thành.");

        public static readonly Error DoctorBusy = new Error(
            "RescheduleAppointment.DoctorBusy",
            "Bác sĩ đã có lịch khám khác vào thời gian này. Vui lòng chọn giờ khác.");

        public static readonly Error DatabaseError = new Error(
            "RescheduleAppointment.DatabaseError",
            "Đã xảy ra lỗi khi lưu thao tác dời lịch vào cơ sở dữ liệu.");
    }
}