using Application.Common;
using AutoMapper;
using DanpheEMR.Core.Domain.Pharmacy;
using DanpheEMR.Core.Interface.Base;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DanpheEMR.Application.Features.Pharmacy.Queries.GetCurrentStock
{
    public class GetCurrentStockQueryHandler : IRequestHandler<GetCurrentStockQuery, Result<List<GetCurrentStockResponse>>>
    {
        // Sử dụng DbSet<Stock> thông qua GenericRepository
        private readonly IGenericRepository<Stock> _stockRepository;
        private readonly IMapper _mapper;

        public GetCurrentStockQueryHandler(IGenericRepository<Stock> stockRepository, IMapper mapper)
        {
            _stockRepository = stockRepository;
            _mapper = mapper;
        }

        public async Task<Result<List<GetCurrentStockResponse>>> Handle(GetCurrentStockQuery request, CancellationToken cancellationToken)
        {
            try
            {
                // Gọi GetAllAsync() nhưng thực tế bạn nên viết 1 hàm riêng trong IStockRepository có .Include(s => s.Item) để Eager Loading
                var allStock = await _stockRepository.GetAllAsync();
                var query = allStock.AsEnumerable();


                if (request.StoreId.HasValue)
                {
                    query = query.Where(s => s.StoreId == request.StoreId.Value);
                }


                if (request.ItemId.HasValue)
                {
                    query = query.Where(s => s.ItemId == request.ItemId.Value);
                }

                query = query.Where(s => s.AvailableQuantity > 0)
                             .OrderBy(s => s.ExpiryDate); 

                var result = _mapper.Map<List<GetCurrentStockResponse>>(query.ToList());
                return Result<List<GetCurrentStockResponse>>.Success(result);
            }
            catch (Exception ex)
            {
                return Result<List<GetCurrentStockResponse>>.Failure(new Error("Stock.Exception", ex.Message));
            }
        }
    }
}