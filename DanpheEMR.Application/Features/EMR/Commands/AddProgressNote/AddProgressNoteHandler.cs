
using DanpheEMR.Application.Features.EMR.Commands.AddDiagnosis;
using DanpheEMR.Core.Interface;
using DanpheEMR.Core.Interface.EMR;
using MediatR;

namespace DanpheEMR.Application.Features.EMR.Commands.AddProgressNote
{
    public class AddProgressNoteHandler : IRequestHandler<AddProgressNoteCommand,Result<Guid>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IProgressNoteRepository _progressNoteRepository;
        public AddProgressNoteHandler(IUnitOfWork unitOfWork, IProgressNoteRepository progressNoteRepository)
        {
            _unitOfWork = unitOfWork;
            _progressNoteRepository = progressNoteRepository;
        }
        public async Task<Result<Guid>> Handle(AddProgressNoteCommand requset, CancellationToken cancellationToken)
        {
            try
            {
                var progressNote = requset.ToEntity();
                await _progressNoteRepository.AddAsync(progressNote);
                var saveResult = await _unitOfWork.SaveChangesAsync(cancellationToken);
                if (saveResult > 0)
                {
                    return Result<Guid>.Success(progressNote.Id);
                }
                return Result<Guid>.Failure(AddDiagnosisErrors.DatabaseError);

            }
            catch (Exception ex)
            {
                return Result<Guid>.Failure(AddDiagnosisErrors.DatabaseError);

            }
        }

    }
}
