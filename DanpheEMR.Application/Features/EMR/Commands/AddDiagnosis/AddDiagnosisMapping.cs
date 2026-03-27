using DanpheEMR.Core.Domain.EMR;

namespace DanpheEMR.Application.Features.EMR.Commands.AddDiagnosis
{
    public static class AddDiagnosisMapping
    {
        public static Diagnosis ToEntity(this AddDiagnosisCommand command)
        {
            return new Diagnosis
            {
                Id = Guid.NewGuid(),
                DiagnosisDate = DateTime.Now, 
                ICD10Code = command.ICD10Code,
                Description = command.Description,
                IsPrimary = command.IsPrimary,

                PatientId = command.PatientId,
                VisitId = command.VisitId,
                ProviderId = command.ProviderId,

                IsDeleted = true 
            };
        }
    }
}