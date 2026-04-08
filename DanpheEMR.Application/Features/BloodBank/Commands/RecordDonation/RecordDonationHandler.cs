using Application.Common;
using DanpheEMR.Application.Abstractions.Persistence;
using DanpheEMR.Core.Interface.BloodBank; // Nơi chứa IBloodInventoryRepository và IBloodDonorRepository
using MediatR;


namespace DanpheEMR.Application.Features.BloodBank.Commands.RecordDonation
{
    public class RecordDonationHandler : IRequestHandler<RecordDonationCommand, Result<Guid>>
    {
        private readonly IBloodDonoreRepository _donorRepository;
        private readonly IBloodInventoryRepository _inventoryRepository;
        private readonly IUnitOfWork _unitOfWork;

        public RecordDonationHandler(
            IBloodDonoreRepository donorRepository,
            IBloodInventoryRepository inventoryRepository,
            IUnitOfWork unitOfWork)
        {
            _donorRepository = donorRepository;
            _inventoryRepository = inventoryRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<Guid>> Handle(RecordDonationCommand request, CancellationToken cancellationToken)
        {
            try
            {
               
                var donor = await _donorRepository.GetByIdAsync(request.DonorId);
                if (donor == null)
                {
                    return Result<Guid>.Failure(RecordDonationErrors.DonorNotFound);
                }

               
                if (!donor.IsEligibleToDonate)
                {
                    return Result<Guid>.Failure(RecordDonationErrors.NotEligible);
                }

               
                donor.TotalDonations += 1;
                donor.LastDonatedDate = DateTime.Now;
                _donorRepository.Update(donor);

                
                var bloodBag = request.ToInventoryEntity(donor);
                await _inventoryRepository.AddAsync(bloodBag);

               
                var saveResult = await _unitOfWork.SaveChangesAsync(cancellationToken);

                if (saveResult > 0)
                {
                    return Result<Guid>.Success(bloodBag.Id);
                }

                return Result<Guid>.Failure(RecordDonationErrors.DatabaseError);
            }
            catch (Exception)
            {
                return Result<Guid>.Failure(RecordDonationErrors.DatabaseError);
            }
        }
    }
}