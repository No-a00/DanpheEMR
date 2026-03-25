using DanpheEMR.Core.Interface;
using DanpheEMR.Core.Interface.BloodBank;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace DanpheEMR.Application.Features.BloodBank.Commands.RegisterDonor
{
    public class RegisterDonorHandler : IRequestHandler<RegisterDonorCommand, Result<RegisterDonorResponse>>
    {
        private readonly IBloodDonoreRepository _bloodDonorRepository;
        private readonly IBloodGroupRepository _bloodGroupRepository;
        private readonly IUnitOfWork _unitOfWork;

        public RegisterDonorHandler(
            IBloodDonoreRepository bloodDonorRepository,
            IBloodGroupRepository bloodGroupRepository,
            IUnitOfWork unitOfWork)
        {
            _bloodDonorRepository = bloodDonorRepository;
            _bloodGroupRepository = bloodGroupRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<RegisterDonorResponse>> Handle(RegisterDonorCommand request, CancellationToken cancellationToken)
        {
            try
            {
                
                var bloodGroup = await _bloodGroupRepository.GetByIdAsync(request.BloodGroupId);
                if (bloodGroup == null)
                {
                    return Result<RegisterDonorResponse>.Failure(RegisterDonorErrors.InvalidBloodType);
                }

                var newDonor = request.ToEntity();
                if (newDonor.Weight < 45)
                {
                    return Result<RegisterDonorResponse>.Failure(RegisterDonorErrors.Underweight);
                }

                // 4. Thêm vào DB (Giả định AddAsync có sẵn trong IGenericRepository)
                await _bloodDonorRepository.AddAsync(newDonor);
                var saveResult = await _unitOfWork.SaveChangesAsync(cancellationToken);

                if (saveResult > 0)
                {
                    var response = new RegisterDonorResponse
                    {
                        Id = newDonor.Id,
                        Message = "Đăng ký người hiến máu thành công!"
                    };
                    return Result<RegisterDonorResponse>.Success(response);
                }

                return Result<RegisterDonorResponse>.Failure(RegisterDonorErrors.DatabaseError);
            }
            catch (Exception)
            {
                return Result<RegisterDonorResponse>.Failure(RegisterDonorErrors.DatabaseError);
            }
        }
    }
}