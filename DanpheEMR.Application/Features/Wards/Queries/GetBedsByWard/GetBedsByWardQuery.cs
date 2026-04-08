
using MediatR;

namespace DanpheEMR.Application.Features.Inpatient.Queries.GetBedsByWard
{
    public record GetBedsByWardQuery(Guid WardId) : IRequest<Result<List<GetBedsByWardResponse>>>;
}