using Application.Common;

namespace DanpheEMR.Application.Features.Patients.Commands.DischargePatient
{
    public static class DischargePatientErrors
    {
        public static readonly Error NotFound = new Error("Discharge.NotFound", "Không tìm thấy hồ sơ bệnh án nội trú này.");
        public static readonly Error DBError = new Error("Discharge.DBError", "Lỗi khi cập nhật trạng thái xuất viện.");
    }
}