using Application.Common;

namespace DanpheEMR.Application.Features.Admin.Commands.AssignRolePermission
{
    public static class AssignRolePermissionErrors
    {
        public static readonly Error RoleOrPermissionNotFound = new Error(
            "AssignRolePermission.NotFound",
            "Không tìm thấy Vai trò (Role) hoặc Quyền (Permission) này trong hệ thống.");

        public static readonly Error PermissionAlreadyAssigned = new Error(
            "AssignRolePermission.AlreadyAssigned",
            "Vai trò này đã được gán quyền này từ trước rồi.");

        public static readonly Error DatabaseError = new Error(
            "AssignRolePermission.DatabaseError",
            "Đã xảy ra lỗi khi lưu thông tin phân quyền.");
    }
}