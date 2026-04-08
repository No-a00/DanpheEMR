using Application.Common;
using MediatR;
using System.Collections.Generic;

namespace DanpheEMR.Application.Features.Pharmacy.Queries.GetPharmacyItems
{
    public record GetPharmacyItemsQuery(string SearchTerm = null) : IRequest<Result<List<GetPharmacyItemsResponse>>>;
}