using Application.Common;
using DanpheEMR.Core.Domain.Wards;
using DanpheEMR.Core.Interface;
using DanpheEMR.Core.Interface.Base;
using MediatR;


namespace DanpheEMR.Application.Features.Inpatient.Commands.UpdateBedStatus
{
    public class UpdateBedStatusHandler : IRequestHandler<UpdateBedStatusCommand, Result<bool>>
    {
        private readonly IGenericRepository<Bed> _bedRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateBedStatusHandler(IGenericRepository<Bed> bedRepository, IUnitOfWork unitOfWork)
        {
            _bedRepository = bedRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<bool>> Handle(UpdateBedStatusCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var bed = await _bedRepository.GetByIdAsync(request.BedId);
                if (bed == null) return Result<bool>.Failure(UpdateBedStatusErrors.NotFound);

                // Cập nhật trạng thái (VD: Chuyển từ Trống sang Có người nằm)
                bed.Status = request.NewStatus;

                _bedRepository.Update(bed);

                var saveResult = await _unitOfWork.SaveChangesAsync(cancellationToken);
                return saveResult > 0 ? Result<bool>.Success(true) : Result<bool>.Failure(UpdateBedStatusErrors.DBError);
            }
            catch (Exception ex)
            {
                return Result<bool>.Failure(new Error("Bed.Exception", $"Lỗi hệ thống: {ex.Message}"));
            }
        }
    }
}