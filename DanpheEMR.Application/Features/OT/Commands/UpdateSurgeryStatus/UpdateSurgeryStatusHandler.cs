using Application.Common;
using DanpheEMR.Application.Abstractions.Persistence;
using DanpheEMR.Core.Enums;
using DanpheEMR.Core.Interface;
using DanpheEMR.Core.Interface.Base;
using DanpheEMR.Core.Interface.OT; // Chứa IOTScheduleRepository
using DanpheEMR.Core.Interfaces.Base; // Chứa ICurrentUserService
using MediatR;
using DanpheEMR.Core.Domain.Admin; 

namespace DanpheEMR.Application.Features.OT.Commands.UpdateSurgeryStatus
{
    public class UpdateSurgeryStatusHandler : IRequestHandler<UpdateSurgeryStatusCommand, Result<bool>>
    {
        private readonly IOTScheduleRepository _otScheduleRepository;
        private readonly IGenericRepository<User> _userRepo;
        private readonly ICurrentUserService _currentUserService; // Lấy ID người đang đăng nhập để gán vào người hủy
        private readonly IUnitOfWork _unitOfWork;

        public UpdateSurgeryStatusHandler(
            IOTScheduleRepository otScheduleRepository,
                IGenericRepository<User> userRepo,
            ICurrentUserService currentUserService,
            IUnitOfWork unitOfWork)
        {
            _otScheduleRepository = otScheduleRepository;
            _userRepo = userRepo;
            _currentUserService = currentUserService;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<bool>> Handle(UpdateSurgeryStatusCommand request, CancellationToken cancellationToken)
        {
            var schedule = await _otScheduleRepository.GetByIdAsync(request.ScheduleId);
            if (schedule == null) return Result<bool>.Failure(UpdateSurgeryStatusErrors.NotFound);

            
            schedule.Status = request.Status;

           
            if (request.Status == OTStatus.Cancelled)
            {
                schedule.IsDeleted = false;
                schedule.Reason = request.CancelReason;
                var userId = _currentUserService.UserId;
                var user = await _userRepo.GetFirstOrDefaultAsync(u => u.Id == userId);
                schedule.DeletedBy = user.Code;
            }

            _otScheduleRepository.Update(schedule);
            var saveResult = await _unitOfWork.SaveChangesAsync(cancellationToken);

            return saveResult > 0 ? Result<bool>.Success(true) : Result<bool>.Failure(UpdateSurgeryStatusErrors.DatabaseError);
        }
    }
}