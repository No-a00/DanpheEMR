using Application.Common;

namespace DanpheEMR.Application.Features.EMR.Commands.RecordVitals
{
    public static class RecordVitalsErrors
    {
        public static readonly Error DatabaseError = new Error(
            "RecordVitals.DatabaseError",
            "Đã xảy ra lỗi khi lưu chỉ số sinh tồn vào cơ sở dữ liệu.");
    }
}