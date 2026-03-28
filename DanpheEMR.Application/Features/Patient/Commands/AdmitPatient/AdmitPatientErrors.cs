using Application.Common;

namespace DanpheEMR.Application.Features.Patients.Commands.AdmitPatient
{
    public static class AdmitPatientErrors
    {
        public static readonly Error DBError = new Error("Admit.DBError", "Lỗi lưu ca nhập viện.");
    }
}