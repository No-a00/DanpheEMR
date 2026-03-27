using System;
using System.Collections.Generic;

namespace DanpheEMR.Application.Features.EMR.Queries.GetPatientMedicalHistory
{
    public class GetPatientMedicalHistoryResponse
    {
        public Guid PatientId { get; set; }
        public int TotalRecords { get; set; }
        public List<MedicalHistoryItemDto> History { get; set; } = new();
    }

    public class MedicalHistoryItemDto
    {
        public Guid DiagnosisId { get; set; }
        public Guid VisitId { get; set; }
        public DateTime DiagnosisDate { get; set; }
        public string ICD10Code { get; set; }
        public string Description { get; set; }
        public bool IsPrimary { get; set; }

        // Tên bác sĩ chẩn đoán
        public string ProviderName { get; set; }
    }
}