using Application.Common;

namespace DanpheEMR.Application.Features.Appointments.Commands.SetupDoctorSchedule
{
    public static class SetupDoctorScheduleErrors
    {
        public static readonly Error InvalidTimeRange = new Error(
            "SetupDoctorSchedule.InvalidTimeRange",
            "Giờ kết thúc ca làm việc phải lớn hơn giờ bắt đầu.");

        public static readonly Error DatabaseError = new Error(
            "SetupDoctorSchedule.DatabaseError",
            "Đã xảy ra lỗi khi lưu lịch làm việc vào cơ sở dữ liệu.");
    }
}