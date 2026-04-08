using Application.Common;
using AutoMapper;
using DanpheEMR.Core.Domain.Pharmacy;
using DanpheEMR.Core.Interface.Base;
using MediatR;

namespace DanpheEMR.Application.Features.Pharmacy.Queries.GetPharmacyItems
{
    public class GetPharmacyItemsQueryHandler : IRequestHandler<GetPharmacyItemsQuery, Result<List<GetPharmacyItemsResponse>>>
    {
        private readonly IGenericRepository<Item> _itemRepository;
        private readonly IMapper _mapper;

        public GetPharmacyItemsQueryHandler(IGenericRepository<Item> itemRepository, IMapper mapper)
        {
            _itemRepository = itemRepository;
            _mapper = mapper;
        }

        public async Task<Result<List<GetPharmacyItemsResponse>>> Handle(GetPharmacyItemsQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var query = await _itemRepository.GetAllAsync();
                var items = query.AsEnumerable();

                if (!string.IsNullOrWhiteSpace(request.SearchTerm))
                {
                    var search = request.SearchTerm.ToLower();
                    items = items.Where(x =>
                        x.ItemName.ToLower().Contains(search) ||
                        x.ItemCode.ToLower().Contains(search) ||
                        (x.GenericName != null && x.GenericName.ToLower().Contains(search)));
                }

                var result = _mapper.Map<List<GetPharmacyItemsResponse>>(items.ToList());
                return Result<List<GetPharmacyItemsResponse>>.Success(result);
            }
            catch (Exception ex)
            {
                return Result<List<GetPharmacyItemsResponse>>.Failure(new Error("PharmacyItem.Exception", ex.Message));
            }
        }
    }
}