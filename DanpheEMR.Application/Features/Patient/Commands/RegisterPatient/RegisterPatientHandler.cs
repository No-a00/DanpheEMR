using Application.Common;
using AutoMapper;
using DanpheEMR.Application.Abstractions.Persistence;
using DanpheEMR.Core.Interface;
using DanpheEMR.Core.Interface.Patients;
using MediatR;
using PatientModel = DanpheEMR.Core.Domain.Patients.Patient;

namespace DanpheEMR.Application.Features.Patients.Commands.RegisterPatient
{
    public class RegisterPatientHandler : IRequestHandler<RegisterPatientCommand, Result<RegisterPatientResponse>>
    {
        private readonly IPatientRepository _patientRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public RegisterPatientHandler(IPatientRepository patientRepository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _patientRepository = patientRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
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
            catch (Exception)
            {
                return Result<RegisterPatientResponse>.Failure(new Error("RegisterPatient.DatabaseError", "Không thể lưu hồ sơ bệnh nhân."));
            }
        }
    }
}