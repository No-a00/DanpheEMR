using Application.Common;
using MediatR;
using System;

namespace DanpheEMR.Application.Features.OT.Commands.ScheduleSurgery
{
    public record ScheduleSurgeryCommand(
        Guid PatientId,
        Guid OTRoomId,
        Guid SurgeonId,
        Guid? AnesthetistId,
        Guid? AdmissionId,
        DateTime SurgeryDate,
        TimeSpan StartTime,
        TimeSpan EndTime,
        string SurgeryType,
        string Remarks
    ) : IRequest<Result<ScheduleSurgeryResponse>>;
}