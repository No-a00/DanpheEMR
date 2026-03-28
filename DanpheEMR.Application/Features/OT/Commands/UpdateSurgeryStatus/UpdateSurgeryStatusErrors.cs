using Application.Common;

namespace DanpheEMR.Application.Features.OT.Commands.UpdateSurgeryStatus
{
    public static class UpdateSurgeryStatusErrors
    {
        public static readonly Error NotFound = new Error("UpdateSurgeryStatus.NotFound", "Không tìm thấy ca mổ này.");
        public static readonly Error MissingCancelReason = new Error("UpdateSurgeryStatus.MissingCancelReason", "Phải nhập lý do khi hủy ca mổ.");
        public static readonly Error DatabaseError = new Error("UpdateSurgeryStatus.DatabaseError", "Lỗi khi cập nhật trạng thái.");
    }
}