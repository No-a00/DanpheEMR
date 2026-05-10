
using AutoMapper;
using DanpheEMR.Application.Abstractions.Persistence;
using DanpheEMR.Core.Domain.Patients;
using Application.Common;
using DanpheEMR.Core.Interface.Patients; 
using MediatR;


namespace DanpheEMR.Application.Features.Patients.Commands.CheckInPatient
{
    public class CheckInPatientHandler : IRequestHandler<CheckInPatientCommand, Result<string>>
    {
        private readonly IPatientRepository _patientRepository;
        private readonly IVisitRepository _visitRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CheckInPatientHandler(IPatientRepository patientRepository, IVisitRepository visitRepository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _patientRepository = patientRepository; _visitRepository = visitRepository; _unitOfWork = unitOfWork; _mapper = mapper;
        }

        public async Task<Result<string>> Handle(CheckInPatientCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var patient = await _patientRepository.GetByPatientCodeAsync(request.PatientCode);
                if (patient == null) return Result<string>.Failure(CheckInPatientErrors.PatientNotFound);

                var visit = _mapper.Map<Visit>(request);
                visit.VisitCode = await _visitRepository.GenerateVisitCodeAsync();
                visit.QueueNo = await _visitRepository.GenerateQueueNoAsync(request.DepartmentCode, DateTime.Now);

                await _visitRepository.AddAsync(visit);

                var saveResult = await _unitOfWork.SaveChangesAsync(cancellationToken);
                return saveResult > 0 ? Result<string>.Success(visit.VisitCode) : Result<string>.Failure(CheckInPatientErrors.DatabaseError);
            }
            catch (Exception ex)
            {
                return Result<string>.Failure(new Error("CheckIn.Exception", $"Lỗi chi tiết: {ex.Message}"));
            }
        }
    }
}