using Application.Common;
using MediatR;
using System;

namespace DanpheEMR.Application.Features.EMR.Commands.CreateDoctorOrder
{
    public record CreateDoctorOrderCommand(
        Guid VisitId,
        Guid ProviderId,
        string OrderText
    ) : IRequest<Result<Guid>>;
}