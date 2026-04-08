
using DanpheEMR.Core.Enums; 
using MediatR;

namespace DanpheEMR.Application.Features.Inpatient.Commands.UpdateBedStatus
{
    public record UpdateBedStatusCommand(
        Guid BedId,
        BedStatus NewStatus
    ) : IRequest<Result<bool>>;
}