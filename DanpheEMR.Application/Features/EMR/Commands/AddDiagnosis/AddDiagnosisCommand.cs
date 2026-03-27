
using MediatR;


namespace DanpheEMR.Application.Features.EMR.Commands.AddDiagnosis
{
    public record AddDiagnosisCommand(
        Guid PatientId,
        Guid VisitId,
        Guid ProviderId,
        string ICD10Code,
        string Description,
        bool IsPrimary
    ) : IRequest<Result<Guid>>;
}