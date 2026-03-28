
using AutoMapper;
using DanpheEMR.Core.Domain.Patients;
using DanpheEMR.Core.Interface;
using DanpheEMR.Core.Interface.Base;
using MediatR;

namespace DanpheEMR.Application.Features.Patients.Commands.AdmitPatient
{
    public class AdmitPatientHandler : IRequestHandler<AdmitPatientCommand, Result<Guid>>
    {
        private readonly IGenericRepository<Admission> _admissionRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public AdmitPatientHandler(IGenericRepository<Admission> admissionRepository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _admissionRepository = admissionRepository; _unitOfWork = unitOfWork; _mapper = mapper;
        }

        public async Task<Result<Guid>> Handle(AdmitPatientCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var admission = _mapper.Map<Admission>(request);
                await _admissionRepository.AddAsync(admission);

                var saveResult = await _unitOfWork.SaveChangesAsync(cancellationToken);
                return saveResult > 0 ? Result<Guid>.Success(admission.Id) : Result<Guid>.Failure(AdmitPatientErrors.DBError);
            }
            catch (Exception)
            {
                return Result<Guid>.Failure(AdmitPatientErrors.DBError);
            }
        }
    }
}