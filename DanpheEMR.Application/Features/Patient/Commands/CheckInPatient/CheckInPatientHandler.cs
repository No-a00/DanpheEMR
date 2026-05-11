
using Application.Common;
using AutoMapper;
using DanpheEMR.Application.Abstractions.Persistence;
using DanpheEMR.Core.Domain.Patients;
using DanpheEMR.Core.Interface.Admin;
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
        private readonly IDepartmentRepository _departmentRepository;

        public CheckInPatientHandler(IPatientRepository patientRepository, IVisitRepository visitRepository, IUnitOfWork unitOfWork, IMapper mapper, IDepartmentRepository departmentRepository )
        {
            _patientRepository = patientRepository; _visitRepository = visitRepository; _unitOfWork = unitOfWork; _mapper = mapper;_departmentRepository = departmentRepository;
        }

        public async Task<Result<string>> Handle(CheckInPatientCommand request, CancellationToken cancellationToken)
        {
            try
            {

                var patient = await _patientRepository.GetFirstOrDefaultAsync(p => p.PatientCode == request.PatientCode);
                if (patient == null) return Result<string>.Failure(new Error("CheckIn", "Không tìm thấy bệnh nhân."));
                //chuyển code thành id để lấy thông tin phòng ban
                var department = await _departmentRepository.GetFirstOrDefaultAsync(d => d.DepartmentCode == request.DepartmentCode);
                if (department == null) return Result<string>.Failure(new Error("CheckIn", "Không tìm thấy phòng ban."));

                if (patient == null) return Result<string>.Failure(CheckInPatientErrors.PatientNotFound);

                var visit = _mapper.Map<Visit>(request);

                // Gán các thông tin cần thiết cho visit
                visit.PatientId = patient.Id;       
                visit.DepartmentId = department.Id;

                visit.VisitCode = await _visitRepository.GenerateVisitCodeAsync();
                visit.QueueNo = await _visitRepository.GenerateQueueNoAsync(request.DepartmentCode, DateTime.Now);

                // Lưu thông tin visit vào database
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