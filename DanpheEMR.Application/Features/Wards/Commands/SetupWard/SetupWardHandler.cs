using Application.Common;
using AutoMapper;
using DanpheEMR.Application.Abstractions.Persistence;
using DanpheEMR.Core.Domain.Admin;
using DanpheEMR.Core.Domain.Wards;
using DanpheEMR.Core.Interface.Base;
using MediatR;


namespace DanpheEMR.Application.Features.Inpatient.Commands.SetupWard
{
    public class SetupWardHandler : IRequestHandler<SetupWardCommand, Result<Guid>>
    {
        private readonly IGenericRepository<Ward> _wardRepository;
        private readonly IGenericRepository<Department> _depRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public SetupWardHandler(IGenericRepository<Ward> wardRepository, IUnitOfWork unitOfWork, IMapper mapper,IGenericRepository<Department> depRepository)
        {
            _wardRepository = wardRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _depRepository = depRepository;
        }

        public async Task<Result<Guid>> Handle(SetupWardCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var departient = await _depRepository.GetFirstOrDefaultAsync(p => p.DepartmentCode == request.DepartmentCode);
                var ward = _mapper.Map<Ward>(request);
                ward.Id = departient.Id;
                await _wardRepository.AddAsync(ward);

                var saveResult = await _unitOfWork.SaveChangesAsync(cancellationToken);
                return saveResult > 0 ? Result<Guid>.Success(ward.Id) : Result<Guid>.Failure(SetupWardErrors.DBError);
            }
            catch (Exception ex)
            {
                return Result<Guid>.Failure(new Error("Ward.Exception", $"Lỗi hệ thống: {ex.Message}"));
            }
        }
    }
}