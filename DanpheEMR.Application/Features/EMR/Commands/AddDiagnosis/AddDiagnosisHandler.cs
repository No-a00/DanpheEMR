
using DanpheEMR.Application.Abstractions.Persistence;
using DanpheEMR.Core.Interface;
using DanpheEMR.Core.Interface.EMR; 
using MediatR;


namespace DanpheEMR.Application.Features.EMR.Commands.AddDiagnosis
{
    public class AddDiagnosisHandler : IRequestHandler<AddDiagnosisCommand, Result<Guid>>
    {
        private readonly IDiagnosisRepository _diagnosisRepository;
        private readonly IUnitOfWork _unitOfWork;

        public AddDiagnosisHandler(
            IDiagnosisRepository diagnosisRepository,
            IUnitOfWork unitOfWork)
        {
            _diagnosisRepository = diagnosisRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<Guid>> Handle(AddDiagnosisCommand request, CancellationToken cancellationToken)
        {
            try
            {
                
                var diagnosis = request.ToEntity();

                
                await _diagnosisRepository.AddAsync(diagnosis);

               
                var saveResult = await _unitOfWork.SaveChangesAsync(cancellationToken);

                if (saveResult > 0)
                {
                    return Result<Guid>.Success(diagnosis.Id);
                }

                return Result<Guid>.Failure(AddDiagnosisErrors.DatabaseError);
            }
            catch (Exception)
            {
                return Result<Guid>.Failure(AddDiagnosisErrors.DatabaseError);
            }
        }
    }
}