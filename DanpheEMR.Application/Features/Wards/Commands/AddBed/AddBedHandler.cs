using Application.Common;
using AutoMapper;
using DanpheEMR.Application.Abstractions.Persistence;
using DanpheEMR.Core.Domain.Wards;
using DanpheEMR.Core.Interface;
using DanpheEMR.Core.Interface.Base;
using DanpheEMR.Core.Interface.Wards;
using MediatR;

namespace DanpheEMR.Application.Features.Inpatient.Commands.AddBed
{
    public class AddBedHandler : IRequestHandler<AddBedCommand, Result<Guid>>
    {
        private readonly IBedRepository _bedRepository;
        private readonly IGenericRepository<BedFeature> _bedFeatureRepository;
        private readonly IGenericRepository<Ward> _wardRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public AddBedHandler(IBedRepository bedRepository, IUnitOfWork unitOfWork, IMapper mapper, IGenericRepository<Ward> wardRepository, IGenericRepository<BedFeature> bedFeatureRepository)
        {
            _bedRepository = bedRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _wardRepository = wardRepository;
            _bedFeatureRepository = bedFeatureRepository;
        }

        public async Task<Result<Guid>> Handle(AddBedCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var bedFeature = await _bedFeatureRepository.GetFirstOrDefaultAsync(p => p.FeatureCode == request.FeatureCode);
                if (bedFeature == null) return Result<Guid>.Failure(new Error("Bed.NotFound", "không tìm thấy loại giường bệnh"));
                var ward = await _wardRepository.GetFirstOrDefaultAsync(p => p.WardCode == request.WardCode);
                if (ward == null) return Result<Guid>.Failure(new Error("Bed.NotFound", "không timg thấy phòng bệnh"));
                var bed = _mapper.Map<Bed>(request);
                bed.WardId = ward.Id;
                bed.BedFeatureId = bedFeature.Id;
                bed.BedCode = await _bedRepository.GenerateBedCodeAsync();


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