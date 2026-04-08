using DanpheEMR.Core.Enums;


namespace DanpheEMR.Application.Features.Inpatient.Queries.GetBedsByWard
{
    public record GetBedsByWardResponse(
        Guid Id,
        string BedNumber,
        string BedCode,
        BedStatus Status 
    );
}