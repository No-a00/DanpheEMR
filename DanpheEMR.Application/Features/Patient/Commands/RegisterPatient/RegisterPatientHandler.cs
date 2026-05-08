using Application.Common;
using AutoMapper;
using DanpheEMR.Application.Abstractions.Persistence;
using DanpheEMR.Core.Interface;
using DanpheEMR.Core.Interface.Patients;
using MediatR;
using Microsoft.Extensions.Logging;
using PatientModel = DanpheEMR.Core.Domain.Patients.Patient;

namespace DanpheEMR.Application.Features.Patients.Commands.RegisterPatient
{
    public class RegisterPatientHandler : IRequestHandler<RegisterPatientCommand, Result<RegisterPatientResponse>>
    {
        private readonly IPatientRepository _patientRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<RegisterPatientHandler> _logger;

        public RegisterPatientHandler(IPatientRepository patientRepository, IUnitOfWork unitOfWork, IMapper mapper,ILogger<RegisterPatientHandler> logger)
        {
            _patientRepository = patientRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<Result<RegisterPatientResponse>> Handle(RegisterPatientCommand request, CancellationToken cancellationToken)
        {
            try
            {
                if (!string.IsNullOrEmpty(request.IdCardNumber))
                {
                    bool isIdExists = await _patientRepository.IsIdCardExistsAsync(request.IdCardNumber);
                    if (isIdExists)
                    {
                        return Result<RegisterPatientResponse>.Failure(new Error("RegisterPatient.IdCardExists", "Căn cước công dân này đã được đăng ký trong hệ thống."));
                    }
                }


                var patient = _mapper.Map<PatientModel>(request);


                patient.PatientCode = await _patientRepository.GeneratePatientCodeAsync();


                await _patientRepository.AddAsync(patient);
                var saveResult = await _unitOfWork.SaveChangesAsync(cancellationToken);

                if (saveResult > 0)
                {
                    var response = new RegisterPatientResponse
                    {
                        Id = patient.Id,
                        PatientCode = patient.PatientCode,
                        FullName = patient.FullName
                    };
                    return Result<RegisterPatientResponse>.Success(response);
                }

                return Result<RegisterPatientResponse>.Failure(new Error("RegisterPatient.DatabaseError", "Không thể lưu hồ sơ bệnh nhân."));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Lỗi nghiêm trọng khi lưu bệnh nhân CCCD: {IdCard}", request.IdCardNumber);
                return Result<RegisterPatientResponse>.Failure(new Error("RegisterPatient.DatabaseError", "Không thể lưu hồ sơ bệnh nhân."));
            }
        }
    }
}