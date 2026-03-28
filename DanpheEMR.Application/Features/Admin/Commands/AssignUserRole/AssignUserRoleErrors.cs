using Application.Common;

namespace DanpheEMR.Application.Features.Admin.Commands.AssignUserRole
{
    public static class AssignUserRoleErrors
    {
        public static readonly Error UserNotFound = new Error(
            "AssignUserRole.UserNotFound",
            "Không tìm thấy thông tin người dùng trong hệ thống.");

        public static readonly Error RoleAlreadyAssigned = new Error(
            "AssignUserRole.RoleAlreadyAssigned",
            "Người dùng này đã được gán vai trò này từ trước rồi.");

        public static readonly Error DatabaseError = new Error(
            "AssignUserRole.DatabaseError",
            "Đã xảy ra lỗi khi lưu thông tin phân quyền.");
    }
}