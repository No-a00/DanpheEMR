using DanpheEMR.Core.Domain.EMR;
using System;

namespace DanpheEMR.Application.Features.EMR.Commands.AddClinicalNote
{
    public static class AddClinicalNoteMapping
    {
       
        public static ClinicalNote ToEntity(this AddClinicalNoteCommand command)
        {
            return new ClinicalNote
            {
                Id = Guid.NewGuid(), 
                ChiefComplaint = command.ChiefComplaint,
                HistoryOfPresentIllness = command.HistoryOfPresentIllness,
                ExaminationNotes = command.ExaminationNotes,
                VisitId = command.VisitId,
                PatientId = command.PatientId,
                ProviderId = command.ProviderId,

                NoteDate = DateTime.Now,
                IsDelete = true,
                VoidReason = null,
                VoidedByUserId = Guid.Empty 
            };
        }
    }
}