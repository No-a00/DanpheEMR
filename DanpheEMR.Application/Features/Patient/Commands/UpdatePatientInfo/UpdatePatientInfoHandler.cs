using Application.Common;
using DanpheEMR.Application.Abstractions.Persistence;
using DanpheEMR.Core.Interface;
using DanpheEMR.Core.Interface.Patients;
using MediatR;


namespace DanpheEMR.Application.Features.Patients.Commands.UpdatePatientInfo
{
    public class UpdatePatientInfoHandler : IRequestHandler<UpdatePatientInfoCommand, Result<bool>>
    {
        private readonly IPatientRepository _patientRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UpdatePatientInfoHandler(IPatientRepository patientRepository, IUnitOfWork unitOfWork)
        {
            _patientRepository = patientRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<bool>> Handle(UpdatePatientInfoCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var patient = await _patientRepository.GetByIdAsync(request.PatientId);
                if (patient == null) return Result<bool>.Failure(UpdatePatientInfoErrors.NotFound);

                
                patient.FirstName = request.FirstName;
                patient.LastName = request.LastName;
                patient.DOB = request.DOB;
                patient.Gender = request.Gender;
                patient.PhoneNumber = request.PhoneNumber;
                patient.BloodGroup = request.BloodGroup;

                _patientRepository.Update(patient);
                var saveResult = await _unitOfWork.SaveChangesAsync(cancellationToken);

                return saveResult > 0 ? Result<bool>.Success(true) : Result<bool>.Failure(UpdatePatientInfoErrors.DBError);
            }
            catch (Exception ex)
            {
                return Result<bool>.Failure(new Error("UpdatePatient.Exception", $"Đã xảy ra lỗi: {ex.Message}"));
            }
        }
    }
}