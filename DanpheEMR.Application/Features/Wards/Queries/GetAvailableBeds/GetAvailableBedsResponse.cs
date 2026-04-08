using System;

namespace DanpheEMR.Application.Features.Inpatient.Queries.GetAvailableBeds
{
    public record GetAvailableBedsResponse(
        Guid Id,
        string BedNumber,
        string BedCode,
        Guid WardId,
        string WardName 
    );
}