using System;

namespace DanpheEMR.Application.Features.Patients.Queries.GetAdmittedPatients
{
    public record GetAdmittedPatientsResponse(
        Guid AdmissionId,
        string PatientCode,
        string PatientName,
        DateTime AdmissionDate,
        string AdmittingDoctor,
        string Status
    );
}