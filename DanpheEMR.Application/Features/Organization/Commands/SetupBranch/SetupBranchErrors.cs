using Application.Common;

namespace DanpheEMR.Application.Features.Admin.Commands.SetupBranch
{
    public static class SetupBranchErrors
    {
        public static readonly Error DatabaseError = new Error(
            "SetupBranch.DatabaseError",
            "Đã xảy ra lỗi khi lưu thông tin chi nhánh vào cơ sở dữ liệu.");

        public static readonly Error BranchNameExists = new Error(
            "SetupBranch.BranchNameExists",
            "Tên chi nhánh này đã tồn tại trong hệ thống.");
    }
}