using Application.Common;
using DanpheEMR.Application.Abstractions.Persistence;
using DanpheEMR.Core.Interface;
using DanpheEMR.Core.Interface.EMR; // Giả định nơi chứa IVitalsRepository
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace DanpheEMR.Application.Features.EMR.Commands.RecordVitals
{
    public class RecordVitalsHandler : IRequestHandler<RecordVitalsCommand, Result<RecordVitalsResponse>>
    {
        private readonly IVitalsRepository _vitalsRepository;
        private readonly IUnitOfWork _unitOfWork;

        public RecordVitalsHandler(
            IVitalsRepository vitalsRepository,
            IUnitOfWork unitOfWork)
        {
            _vitalsRepository = vitalsRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<RecordVitalsResponse>> Handle(RecordVitalsCommand request, CancellationToken cancellationToken)
        {
            try
            {
                
                var vitalsEntity = request.ToEntity();

                // 2. Thêm vào Repository
                await _vitalsRepository.AddAsync(vitalsEntity);

                // 3. Lưu thay đổi xuống Database (Sẽ tự động sinh AuditLog nhờ UnitOfWork trước đó)
                var saveResult = await _unitOfWork.SaveChangesAsync(cancellationToken);

                if (saveResult > 0)
                {
                    var response = new RecordVitalsResponse
                    {
                        Id = vitalsEntity.Id,
                        Message = "Ghi nhận chỉ số sinh tồn thành công!"
                    };
                    return Result<RecordVitalsResponse>.Success(response);
                }

                return Result<RecordVitalsResponse>.Failure(RecordVitalsErrors.DatabaseError);
            }
            catch (Exception)
            {
                return Result<RecordVitalsResponse>.Failure(RecordVitalsErrors.DatabaseError);
            }
        }
    }
}