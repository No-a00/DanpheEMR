using Application.Common;
using DanpheEMR.Core.Enums;
using DanpheEMR.Core.Interface;
using DanpheEMR.Core.Interface.BloodBank;
using DanpheEMR.Core.Interfaces.Base;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace DanpheEMR.Application.Features.BloodBank.Commands.IssueBlood
{
    public class IssueBloodHandler : IRequestHandler<IssueBloodCommand, Result<bool>>
    {
        private readonly IBloodInventoryRepository _inventoryRepository;
        private readonly IBloodIssueRepository _issueRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICurrentUserService _currentUserService;

        public IssueBloodHandler(
            IBloodInventoryRepository inventoryRepository,
            IBloodIssueRepository issueRepository,
            IUnitOfWork unitOfWork,
            ICurrentUserService currentUserService)
        {
            _inventoryRepository = inventoryRepository;
            _issueRepository = issueRepository;
            _unitOfWork = unitOfWork;
            _currentUserService = currentUserService;
        }

        public async Task<Result<bool>> Handle(IssueBloodCommand request, CancellationToken cancellationToken)
        {
            try
            {
                
                var availableBags = await _inventoryRepository.GetAvailableBagsAsync(request.BloodGroupId, request.Quantity);

               
                if (availableBags.Count < request.Quantity)
                {
                    return Result<bool>.Failure(IssueBloodErrors.OutOfStock);
                }

                var userId = _currentUserService.UserId;

                
                foreach (var bag in availableBags)
                {
                    
                    bag.Status = BloodBagStatus.Issued;
                    _inventoryRepository.Update(bag);

                    
                    var issueRecord = request.ToIssueEntity(bag, userId);
                    await _issueRepository.AddAsync(issueRecord);
                }

               
                var saveResult = await _unitOfWork.SaveChangesAsync(cancellationToken);

                if (saveResult > 0)
                {
                    return Result<bool>.Success(true); 
                }

                return Result<bool>.Failure(IssueBloodErrors.DatabaseError);
            }
            catch (Exception)
            {
                return Result<bool>.Failure(IssueBloodErrors.DatabaseError);
            }
        }
    }
}