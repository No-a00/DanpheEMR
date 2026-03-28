using Application.Common;

namespace DanpheEMR.Application.Features.Patients.Commands.UpdatePatientInfo
{
    public static class UpdatePatientInfoErrors
    {
        public static readonly Error NotFound = new Error("UpdatePatient.NotFound", "Không tìm thấy bệnh nhân trong hệ thống.");
        public static readonly Error DBError = new Error("UpdatePatient.DBError", "Lỗi khi cập nhật thông tin bệnh nhân.");
    }
}