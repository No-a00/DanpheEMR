using Application.Common;
using MediatR;
using System;

namespace DanpheEMR.Application.Features.BloodBank.Queries.GetBloodInventory
{
    public record GetBloodInventoryQuery(
        Guid? BloodGroupId 
    ) : IRequest<Result<GetBloodInventoryResponse>>;
}