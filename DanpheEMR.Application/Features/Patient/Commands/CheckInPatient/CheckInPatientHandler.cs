
using AutoMapper;
using DanpheEMR.Application.Abstractions.Persistence;
using DanpheEMR.Core.Domain.Patients;
using DanpheEMR.Core.Interface;
using DanpheEMR.Core.Interface.Patients; 
using MediatR;


namespace DanpheEMR.Application.Features.Patients.Commands.CheckInPatient
{
    public class CheckInPatientHandler : IRequestHandler<CheckInPatientCommand, Result<Guid>>
    {
        private readonly IPatientRepository _patientRepository;
        private readonly IVisitRepository _visitRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CheckInPatientHandler(IPatientRepository patientRepository, IVisitRepository visitRepository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _patientRepository = patientRepository; _visitRepository = visitRepository; _unitOfWork = unitOfWork; _mapper = mapper;
        }

        public async Task<Result<Guid>> Handle(CheckInPatientCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var patient = await _patientRepository.GetByIdAsync(request.PatientId);
                if (patient == null) return Result<Guid>.Failure(CheckInPatientErrors.PatientNotFound);

                var visit = _mapper.Map<Visit>(request);
                visit.VisitCode = await _visitRepository.GenerateVisitCodeAsync();
                visit.QueueNo = await _visitRepository.GenerateQueueNoAsync(request.DepartmentId, DateTime.Now);

                await _visitRepository.AddAsync(visit);

                var saveResult = await _unitOfWork.SaveChangesAsync(cancellationToken);
                return saveResult > 0 ? Result<Guid>.Success(visit.Id) : Result<Guid>.Failure(CheckInPatientErrors.DatabaseError);
            }
            catch (Exception)
            {

                return Result<Guid>.Failure(CheckInPatientErrors.DatabaseError);


            }
        }
    }
}