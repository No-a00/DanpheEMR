
using MediatR;

namespace DanpheEMR.Application.Features.Inpatient.Queries.GetAvailableBeds
{
    public record GetAvailableBedsQuery(Guid? WardId = null) : IRequest<Result<List<GetAvailableBedsResponse>>>;
}