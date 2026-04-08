using Application.Common;
using AutoMapper;
using DanpheEMR.Application.Abstractions.Persistence;
using DanpheEMR.Core.Domain.Patients;
using DanpheEMR.Core.Interface;
using DanpheEMR.Core.Interface.Patients;
using MediatR;


namespace DanpheEMR.Application.Features.Patients.Commands.TransferPatient
{
    public class TransferPatientHandler : IRequestHandler<TransferPatientCommand, Result<Guid>>
    {
        private readonly ITransferRepository _transferRepository;
        private readonly IAdmissionRepository _admissionRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public TransferPatientHandler(
            ITransferRepository transferRepository,
            IAdmissionRepository admissionRepository,
            IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _transferRepository = transferRepository;
            _admissionRepository = admissionRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<Guid>> Handle(TransferPatientCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var admission = await _admissionRepository.GetByIdAsync(request.AdmissionId);
                if (admission == null) return Result<Guid>.Failure(TransferPatientErrors.AdmissionNotFound);

                var transfer = _mapper.Map<Transfer>(request);

                await _transferRepository.AddAsync(transfer);
                var saveResult = await _unitOfWork.SaveChangesAsync(cancellationToken);

                return saveResult > 0 ? Result<Guid>.Success(transfer.Id) : Result<Guid>.Failure(TransferPatientErrors.DBError);
            }
            catch (Exception ex)
            {
                return Result<Guid>.Failure(new Error("Transfer.Exception", $"Đã xảy ra lỗi: {ex.Message}"));
            }
        }
    }
}