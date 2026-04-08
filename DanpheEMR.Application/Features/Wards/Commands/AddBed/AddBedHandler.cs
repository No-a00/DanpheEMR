using Application.Common;
using AutoMapper;
using DanpheEMR.Core.Domain.Wards;
using DanpheEMR.Core.Interface;
using DanpheEMR.Core.Interface.Base;
using MediatR;

namespace DanpheEMR.Application.Features.Inpatient.Commands.AddBed
{
    public class AddBedHandler : IRequestHandler<AddBedCommand, Result<Guid>>
    {
        private readonly IGenericRepository<Bed> _bedRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public AddBedHandler(IGenericRepository<Bed> bedRepository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _bedRepository = bedRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<Guid>> Handle(AddBedCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var bed = _mapper.Map<Bed>(request);
                await _bedRepository.AddAsync(bed);

                var saveResult = await _unitOfWork.SaveChangesAsync(cancellationToken);
                return saveResult > 0 ? Result<Guid>.Success(bed.Id) : Result<Guid>.Failure(AddBedErrors.DBError);
            }
            catch (Exception ex)
            {
                return Result<Guid>.Failure(new Error("Bed.Exception", $"Lỗi hệ thống: {ex.Message}"));
            }
        }
    }
}