using MediatR;
namespace DanpheEMR.Application.Features.EMR.Commands.AddClinicalNote
{
    public record AddClinicalNoteCommand(
        string ChiefComplaint,
        string HistoryOfPresentIllness,
        string ExaminationNotes,
        Guid VisitId,
        Guid PatientId,
        Guid ProviderId
    ) : IRequest<Result<AddClinicalNoteResponse>>; 
}