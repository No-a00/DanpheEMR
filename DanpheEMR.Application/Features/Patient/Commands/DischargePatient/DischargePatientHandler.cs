using Application.Common;
using DanpheEMR.Core.Domain.Patients;
using DanpheEMR.Core.Enums;
using DanpheEMR.Core.Interface;
using DanpheEMR.Core.Interface.Base;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace DanpheEMR.Application.Features.Patients.Commands.DischargePatient
{
    public class DischargePatientHandler : IRequestHandler<DischargePatientCommand, Result<bool>>
    {
        private readonly IGenericRepository<Admission> _admissionRepository;
        private readonly IGenericRepository<Discharge> _dischargeRepository;
        private readonly IUnitOfWork _unitOfWork;

        public DischargePatientHandler(
            IGenericRepository<Admission> admissionRepository,
            IGenericRepository<Discharge> dischargeRepository,
            IUnitOfWork unitOfWork)
        {
            _admissionRepository = admissionRepository;
            _dischargeRepository = dischargeRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<bool>> Handle(DischargePatientCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var admission = await _admissionRepository.GetByIdAsync(request.AdmissionId);
                if (admission == null) return Result<bool>.Failure(DischargePatientErrors.NotFound);

                var discharge = new Discharge
                {
                    Id = Guid.NewGuid(),
                    DischargeDate = DateTime.Now,
                    DischargeCondition = request.DischargeCondition,
                    DischargeNotes = request.DischargeNotes,
                    IsDeleted = false,
                    PatientId = admission.PatientId,
                    AdmissionId = admission.Id
                };

                await _dischargeRepository.AddAsync(discharge);

                admission.Status = AdmissionStatus.Discharged;
                _admissionRepository.Update(admission);

                var saveResult = await _unitOfWork.SaveChangesAsync(cancellationToken);

                return saveResult > 0
                    ? Result<bool>.Success(true)
                    : Result<bool>.Failure(DischargePatientErrors.DBError);
            }
            catch (Exception ex)
            {
                return Result<bool>.Failure(new Error("Discharge.Exception", $"Đã xảy ra lỗi hệ thống: {ex.Message}"));
            }
        }
    }
}