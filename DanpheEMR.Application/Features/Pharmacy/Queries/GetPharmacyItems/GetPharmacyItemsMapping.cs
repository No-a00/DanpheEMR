using AutoMapper;
using DanpheEMR.Core.Domain.Pharmacy;

namespace DanpheEMR.Application.Features.Pharmacy.Queries.GetPharmacyItems
{
    public class GetPharmacyItemsMapping : Profile
    {
        public GetPharmacyItemsMapping()
        {
            CreateMap<Item, GetPharmacyItemsResponse>();
        }
    }
}