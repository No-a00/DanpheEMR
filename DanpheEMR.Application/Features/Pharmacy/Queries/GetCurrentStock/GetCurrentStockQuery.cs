using Application.Common;
using MediatR;
using System;
using System.Collections.Generic;

namespace DanpheEMR.Application.Features.Pharmacy.Queries.GetCurrentStock
{
    public record GetCurrentStockQuery(
        Guid? StoreId = null, 
        Guid? ItemId = null  
    ) : IRequest<Result<List<GetCurrentStockResponse>>>;
}