using Application.Common;

namespace DanpheEMR.Application.Features.Admin.Commands.RegisterEmployee
{
    public static class RegisterEmployeeErrors
    {
        public static readonly Error DepartmentNotFound = new Error(
            "RegisterEmployee.DepartmentNotFound",
            "Phòng ban bạn chọn không tồn tại trong hệ thống.");


        public static readonly Error DatabaseError = new Error(
            "RegisterEmployee.DatabaseError",
            "Đã xảy ra lỗi khi lưu thông tin nhân viên vào cơ sở dữ liệu.");
    }
}