using Application.Common;
using MediatR;

namespace DanpheEMR.Application.Features.EMR.Queries.GetPendingDoctorOrders
{
    public record GetPendingDoctorOrdersQuery() : IRequest<Result<GetPendingDoctorOrdersResponse>>;
}