using Application.Common;

namespace DanpheEMR.Application.Features.Patients.Commands.CheckInPatient
{
    public static class CheckInPatientErrors
    {
        public static readonly Error DatabaseError = new Error("CheckIn.DBError", "Lỗi khi tạo lượt khám.");
        public static readonly Error PatientNotFound = new Error("CheckIn.PatientNotFound", "Không tìm thấy hồ sơ bệnh nhân.");
    }
}