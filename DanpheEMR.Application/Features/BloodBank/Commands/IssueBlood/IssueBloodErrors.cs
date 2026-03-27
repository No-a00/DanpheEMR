using Application.Common;

namespace DanpheEMR.Application.Features.BloodBank.Commands.IssueBlood
{
    public static class IssueBloodErrors
    {
        public static readonly Error OutOfStock = new Error(
            "IssueBlood.OutOfStock",
            "Không đủ số lượng máu sẵn sàng trong kho cho nhóm máu này.");

        public static readonly Error DatabaseError = new Error(
            "IssueBlood.DatabaseError",
            "Đã xảy ra lỗi khi lưu thông tin xuất máu vào hệ thống.");
    }
}