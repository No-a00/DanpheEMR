using MediatR;

namespace DanpheEMR.Application.Features.EMR.Commands.AddProgressNote
{
    public record AddProgressNoteCommand(
        Guid ProviderId,
        Guid AdmissionId,
        string Title,
        string Subjective,
        string Objective,
        string Assessment,
        string Plan

        ) : IRequest<Result<Guid>>;
}
