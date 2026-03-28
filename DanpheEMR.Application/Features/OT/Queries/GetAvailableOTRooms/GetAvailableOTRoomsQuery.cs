using Application.Common;
using MediatR;
using System.Collections.Generic;

namespace DanpheEMR.Application.Features.OT.Queries.GetAvailableOTRooms
{
    public record GetAvailableOTRoomsQuery() : IRequest<Result<List<GetAvailableOTRoomsResponse>>>;
}