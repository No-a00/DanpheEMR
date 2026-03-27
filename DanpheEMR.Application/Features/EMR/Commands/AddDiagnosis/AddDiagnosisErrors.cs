using Application.Common;

namespace DanpheEMR.Application.Features.EMR.Commands.AddDiagnosis
{
    public static class AddDiagnosisErrors
    {
        public static readonly Error DatabaseError = new Error(
            "AddDiagnosis.DatabaseError",
            "Đã xảy ra lỗi khi lưu chẩn đoán vào hồ sơ bệnh án.");

        public static readonly Error InvalidICD10 = new Error(
            "AddDiagnosis.InvalidICD10",
            "Mã ICD-10 không hợp lệ.");
    }
}