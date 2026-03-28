using Application.Common;

namespace DanpheEMR.Application.Features.Patients.Queries.GetPatientById
{
    public static class GetPatientByIdErrors
    {
        public static readonly Error NotFound = new Error("Patient.NotFound", "Không tìm thấy hồ sơ bệnh nhân này.");
    }
}