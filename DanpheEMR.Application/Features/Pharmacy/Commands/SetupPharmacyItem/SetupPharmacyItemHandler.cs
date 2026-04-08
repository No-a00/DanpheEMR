using Application.Common;
using AutoMapper;
using DanpheEMR.Application.Abstractions.Persistence;
using DanpheEMR.Core.Domain.Pharmacy;
using DanpheEMR.Core.Interface.Base;
using MediatR;

namespace DanpheEMR.Application.Features.Pharmacy.Commands.SetupPharmacyItem
{
    public class SetupPharmacyItemHandler : IRequestHandler<SetupPharmacyItemCommand, Result<Guid>>
    {
        private readonly IGenericRepository<Item> _itemRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public SetupPharmacyItemHandler(IGenericRepository<Item> itemRepository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _itemRepository = itemRepository; _unitOfWork = unitOfWork; _mapper = mapper;
        }

        public async Task<Result<Guid>> Handle(SetupPharmacyItemCommand request, CancellationToken cancellationToken)
        {
            try
            {
               
                var item = _mapper.Map<Item>(request);
                await _itemRepository.AddAsync(item);

                return await _unitOfWork.SaveChangesAsync(cancellationToken) > 0
                    ? Result<Guid>.Success(item.Id)
                    : Result<Guid>.Failure(new Error("Item.DBError", "Lỗi tạo danh mục thuốc."));
            }
            catch (Exception ex) { return Result<Guid>.Failure(new Error("Item.Ex", ex.Message)); }
        }
    }
}