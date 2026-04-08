using Application.Common; // Đảm bảo đúng namespace chứa Result<>
using DanpheEMR.Application.Abstractions.Persistence;
using DanpheEMR.Core.Interfaces.EMR;
using MediatR;


namespace DanpheEMR.Application.Features.EMR.Commands.AddClinicalNote
{
    public class AddClinicalNoteHandler : IRequestHandler<AddClinicalNoteCommand, Result<AddClinicalNoteResponse>>
    {
        private readonly IClinicalNoteRepository _clinicalNoteRepository;
        private readonly IUnitOfWork _unitOfWork;
        public AddClinicalNoteHandler(
            IClinicalNoteRepository clinicalNoteRepository,
            IUnitOfWork unitOfWork)
        {
            _clinicalNoteRepository = clinicalNoteRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<AddClinicalNoteResponse>> Handle(AddClinicalNoteCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var newNote = request.ToEntity();
                await _clinicalNoteRepository.AddAsync(newNote);
                var saveResult = await _unitOfWork.SaveChangesAsync(cancellationToken);

                if (saveResult > 0)
                {
                    var response = new AddClinicalNoteResponse
                    {
                        Id = newNote.Id,
                        Message = "Thêm ghi chú lâm sàng thành công!"
                    };
                    return Result<AddClinicalNoteResponse>.Success(response);
                }
                return Result<AddClinicalNoteResponse>.Failure(AddClinicalNoteErrors.DatabaseError);
            }
            catch (Exception)
            {
                // Bắt các ngoại lệ (Exception) hệ thống
                return Result<AddClinicalNoteResponse>.Failure(AddClinicalNoteErrors.DatabaseError);
            }
        }
    }
}