using Application.Common;

namespace DanpheEMR.Application.Features.Admin.Commands.CreateRole
{
    public static class CreateRoleErrors
    {
        public static readonly Error RoleNameExists = new Error(
            "CreateRole.RoleNameExists",
            "Tên vai trò này đã tồn tại trong hệ thống. Vui lòng chọn tên khác.");

        public static readonly Error DatabaseError = new Error(
            "CreateRole.DatabaseError",
            "Đã xảy ra lỗi khi tạo vai trò mới.");
    }
}