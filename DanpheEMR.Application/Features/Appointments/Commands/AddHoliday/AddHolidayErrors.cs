using Application.Common;

namespace DanpheEMR.Features.Appointment.Commands.AddHoliday
{
    public static class AddHolidayErrors
    {
        public static readonly Error DatabaseError = new Error(
            "AddHoliday.DatabaseError",
            "Đã xảy ra lỗi khi lưu ngày nghỉ vào cơ sở dữ liệu.");

        public static readonly Error InvalidDate = new Error(
            "AddHoliday.InvalidDate",
            "Ngày nghỉ không thể là một ngày trong quá khứ.");
    }
}