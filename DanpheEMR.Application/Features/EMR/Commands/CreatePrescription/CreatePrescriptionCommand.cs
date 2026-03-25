using Application.Common;
using MediatR;
using System;
using System.Collections.Generic;

namespace DanpheEMR.Application.Features.EMR.Commands.CreatePrescription
{
   
    public record PrescriptionItemDto(
        Guid MedicineId, 
        string Dosage,
        string Frequency,
        int DurationInDays,
        string Notes
    );

    public record CreatePrescriptionCommand(
        string Notes,
        Guid VisitId,
        Guid PatientId,
        Guid PrescriberId,
        List<PrescriptionItemDto> Items
    ) : IRequest<Result<CreatePrescriptionResponse>>;
}