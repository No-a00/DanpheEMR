using System;

namespace DanpheEMR.Application.Features.Inpatient.Queries.GetWardOccupancy
{
    public record GetWardOccupancyResponse(
        Guid WardId,
        string WardName,
        int TotalBeds,
        int OccupiedBeds,
        int AvailableBeds,
        double OccupancyRate 
    );
}