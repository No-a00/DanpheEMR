
using DanpheEMR.Core.Domain.EMR;
namespace DanpheEMR.Application.Features.EMR.Commands.AddProgressNote
{
    public static class AddProgressNoteMapping
    {
        public static ProgressNote ToEntity(this AddProgressNoteCommand command)
        {
            return new ProgressNote
            {
                Id = Guid.NewGuid(),
                Title = command.Title,

                NoteDate = DateTime.Now,

                Subjective = command.Subjective,

                Objective = command.Objective,

                Assessment = command.Assessment,

                Plan = command.Plan,



                IsDeleted =false,


                AdmissionId = command.AdmissionId,
                ProviderId = command.ProviderId,

            };
        }
    }
}
