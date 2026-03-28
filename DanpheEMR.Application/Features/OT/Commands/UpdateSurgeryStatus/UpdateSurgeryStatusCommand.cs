
using DanpheEMR.Core.Enums; 
using MediatR;
using System;

namespace DanpheEMR.Application.Features.OT.Commands.UpdateSurgeryStatus
{
    public record UpdateSurgeryStatusCommand(
        Guid ScheduleId,
        OTStatus Status,
        string CancelReason = null 
    ) : IRequest<Result<bool>>;
}