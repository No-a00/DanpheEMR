
using DomainDiagnosis = DanpheEMR.Core.Domain.EMR.Diagnosis;

namespace DanpheEMR.Application.Features.EMR.Queries.GetPatientMedicalHistory
{
    public static class GetPatientMedicalHistoryMapping
    {
        public static MedicalHistoryItemDto ToDto(this DomainDiagnosis diagnosis)
        {
            return new MedicalHistoryItemDto
            {
                DiagnosisId = diagnosis.Id,
                VisitId = diagnosis.VisitId,
                DiagnosisDate = diagnosis.DiagnosisDate,
                ICD10Code = diagnosis.ICD10Code,
                Description = diagnosis.Description,
                IsPrimary = diagnosis.IsPrimary,

                
                ProviderName = diagnosis.Provider != null ? $"{diagnosis.Provider.FirstName} {diagnosis.Provider.LastName}" : "N/A"
            };
        }

        public static List<MedicalHistoryItemDto> ToDtoList(this IEnumerable<DomainDiagnosis> diagnoses)
        {
            return diagnoses.Select(d => d.ToDto()).ToList();
        }
    }
}