using Application.Common;

namespace DanpheEMR.Application.Features.Admin.Commands.SetupDepartment
{
    public static class SetupDepartmentErrors
    {
        public static readonly Error CodeExists = new Error(
            "SetupDepartment.CodeExists",
            "Mã khoa/phòng này đã tồn tại trong hệ thống.");

        public static readonly Error ParentNotFound = new Error(
            "SetupDepartment.ParentNotFound",
            "Khoa/phòng cha được chọn không tồn tại.");

        public static readonly Error HeadNotFound = new Error(
            "SetupDepartment.HeadNotFound",
            "Nhân viên được chọn làm Trưởng khoa không tồn tại.");

        public static readonly Error DatabaseError = new Error(
            "SetupDepartment.DatabaseError",
            "Đã xảy ra lỗi khi lưu thông tin khoa/phòng vào cơ sở dữ liệu.");
    }
}