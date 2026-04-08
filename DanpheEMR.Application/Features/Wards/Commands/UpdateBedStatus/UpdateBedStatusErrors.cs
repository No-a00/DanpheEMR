using Application.Common;

namespace DanpheEMR.Application.Features.Inpatient.Commands.UpdateBedStatus
{
    public static class UpdateBedStatusErrors
    {
        public static readonly Error NotFound = new Error("Bed.NotFound", "Không tìm thấy giường bệnh này.");
        public static readonly Error DBError = new Error("Bed.UpdateError", "Lỗi khi cập nhật trạng thái giường.");
    }
}