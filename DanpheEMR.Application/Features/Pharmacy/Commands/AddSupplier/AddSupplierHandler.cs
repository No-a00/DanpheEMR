using Application.Common;
using AutoMapper;
using DanpheEMR.Application.Abstractions.Persistence;
using DanpheEMR.Core.Domain.Pharmacy;
using DanpheEMR.Core.Interface;
using DanpheEMR.Core.Interface.Base;
using MediatR;

namespace DanpheEMR.Application.Features.Pharmacy.Commands.AddSupplier
{
    public class AddSupplierHandler : IRequestHandler<AddSupplierCommand, Result<Guid>>
    {
        private readonly IGenericRepository<Supplier> _supplierRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public AddSupplierHandler(IGenericRepository<Supplier> supplierRepository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _supplierRepository = supplierRepository; _unitOfWork = unitOfWork; _mapper = mapper;
        }

        public async Task<Result<Guid>> Handle(AddSupplierCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var supplier = _mapper.Map<Supplier>(request);
                await _supplierRepository.AddAsync(supplier);
                return await _unitOfWork.SaveChangesAsync(cancellationToken) > 0 ? Result<Guid>.Success(supplier.Id) : Result<Guid>.Failure(AddSupplierErrors.DBError);
            }
            catch (Exception ex) { return Result<Guid>.Failure(new Error("Supplier.Ex", ex.Message)); }
        }
    }
}