using System;
using System.Collections.Generic;

namespace DanpheEMR.Application.Features.Patients.Queries.GetPatientHistory
{
    public record PatientHistoryResponse(
        List<VisitHistoryDto> Visits,
        List<AdmissionHistoryDto> Admissions
    );

    public record VisitHistoryDto(Guid Id, DateTime Date, string DeptName, string ProviderName, string Status);
    public record AdmissionHistoryDto(Guid Id, DateTime AdmitDate, DateTime? DischargeDate, string Status);
}