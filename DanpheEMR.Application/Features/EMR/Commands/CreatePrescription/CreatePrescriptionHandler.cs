
using DanpheEMR.Core.Interface;
using DanpheEMR.Core.Interface.EMR; 
using MediatR;
namespace DanpheEMR.Application.Features.EMR.Commands.CreatePrescription
{
    public class CreatePrescriptionHandler : IRequestHandler<CreatePrescriptionCommand, Result<CreatePrescriptionResponse>>
    {
        private readonly IPrescriptionRepository _prescriptionRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreatePrescriptionHandler(
            IPrescriptionRepository prescriptionRepository,
            IUnitOfWork unitOfWork)
        {
            _prescriptionRepository = prescriptionRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<CreatePrescriptionResponse>> Handle(CreatePrescriptionCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var newPrescription = request.ToEntity();

                await _prescriptionRepository.AddAsync(newPrescription);

                var saveResult = await _unitOfWork.SaveChangesAsync(cancellationToken);

                if (saveResult > 0)
                {
                    
                    var response = new CreatePrescriptionResponse
                    {
                        Id = newPrescription.Id,
                        Message = "Tạo đơn thuốc thành công!"
                    };
                    return Result<CreatePrescriptionResponse>.Success(response);
                }

                return Result<CreatePrescriptionResponse>.Failure(CreatePrescriptionErrors.DatabaseError);
            }
            catch (Exception)
            {
                return Result<CreatePrescriptionResponse>.Failure(CreatePrescriptionErrors.DatabaseError);
            }
        }
    }
}