using AutoMapper;
using DanpheEMR.Application.Abstractions.Persistence;
using DanpheEMR.Application.Features.Patients.Commands.AdmitPatient;
using DanpheEMR.Core.Domain.Admin;
using DanpheEMR.Core.Domain.Patients;
using DanpheEMR.Core.Interface.Base;
using MediatR;
using Application.Common;
using DanpheEMR.Core.Enums;

public class AdmitPatientHandler : IRequestHandler<AdmitPatientCommand, Result<Guid>>
{
    private readonly IGenericRepository<Admission> _admissionRepository;
    private readonly IGenericRepository<Patient> _patientRepository;
    private readonly IGenericRepository<Employee> _employeeRepository;
    private readonly IGenericRepository<Visit> _visitRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    // Nhớ inject thêm các Repository vào Constructor nhé
    public AdmitPatientHandler(
        IGenericRepository<Admission> admissionRepository,
        IGenericRepository<Patient> patientRepository,
        IGenericRepository<Employee> employeeRepository,
        IGenericRepository<Visit> visitRepository,
        IUnitOfWork unitOfWork,
        IMapper mapper)
    {
        _admissionRepository = admissionRepository; _patientRepository = patientRepository;
        _employeeRepository = employeeRepository; _visitRepository = visitRepository;
        _unitOfWork = unitOfWork; _mapper = mapper;
    }

    public async Task<Result<Guid>> Handle(AdmitPatientCommand request, CancellationToken cancellationToken)
    {
        try
        {
            // 
            var patient = await _patientRepository.GetFirstOrDefaultAsync(p => p.PatientCode == request.PatientCode);
            if (patient == null) return Result<Guid>.Failure(new Error("Admit.Error", "Không tìm thấy Bệnh nhân."));

            // 
            var doctor = await _employeeRepository.GetFirstOrDefaultAsync(d => d.Code == request.AdmittingDoctorCode);
            if (doctor == null) return Result<Guid>.Failure(new Error("Admit.Error", "Không tìm thấy Bác sĩ."));

            //  hiện tại của bệnh nhân (Bắt buộc phải có lượt khám mới được nhập viện)
            var currentVisit = await _visitRepository.GetFirstOrDefaultAsync(v => v.PatientId == patient.Id && v.Status == VisitStatus.Registered); // Hoặc trạng thái tương ứng
            if (currentVisit == null) return Result<Guid>.Failure(new Error("Admit.Error", "Bệnh nhân chưa đăng ký khám hoặc đã kết thúc khám."));

            // 4. Map dữ liệu và Gán ID
            var admission = _mapper.Map<Admission>(request);
            admission.PatientId = patient.Id;
            admission.AdmittingDoctorId = doctor.Id;
            admission.VisitId = currentVisit.Id;

            //
            admission.Code = "ADM-" + DateTime.Now.ToString("yyyyMMddHHmmss"); //có thể thay bằng hàm sinh riêng

            await _admissionRepository.AddAsync(admission);
            var saveResult = await _unitOfWork.SaveChangesAsync(cancellationToken);

            return saveResult > 0 ? Result<Guid>.Success(admission.Id) : Result<Guid>.Failure(AdmitPatientErrors.DBError);
        }
        catch (Exception ex)
        {
            // Bắt lỗi sâu như bài trước để dễ debug
            Exception realError = ex;
            while (realError.InnerException != null) realError = realError.InnerException;
            return Result<Guid>.Failure(new Error("Admit.Exception", $"Lỗi DB: {realError.Message}"));
        }
    }
}