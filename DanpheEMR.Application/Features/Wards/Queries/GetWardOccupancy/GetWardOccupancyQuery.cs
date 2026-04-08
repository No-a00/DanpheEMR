
using MediatR;

namespace DanpheEMR.Application.Features.Inpatient.Queries.GetWardOccupancy
{
    public record GetWardOccupancyQuery(Guid WardId) : IRequest<Result<GetWardOccupancyResponse>>;
}