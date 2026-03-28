using Application.Common;
using AutoMapper;
using DanpheEMR.Core.Domain.Admin;
using DanpheEMR.Core.Interface;
using DanpheEMR.Core.Interface.Admin; 
using MediatR;


namespace DanpheEMR.Application.Features.Admin.Commands.SetupBranch
{
    public class SetupBranchHandler : IRequestHandler<SetupBranchCommand, Result<Guid>>
    {
        private readonly IBranchRepository _branchRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public SetupBranchHandler(
            IBranchRepository branchRepository,
            IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _branchRepository = branchRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<Guid>> Handle(SetupBranchCommand request, CancellationToken cancellationToken)
        {
            try
            {

                bool isExists = await _branchRepository.IsBranchNameExistsAsync(request.BranchName);
                if (isExists) return Result<Guid>.Failure(SetupBranchErrors.BranchNameExists);


                var branch = _mapper.Map<Branch>(request);

                await _branchRepository.AddAsync(branch);

                var saveResult = await _unitOfWork.SaveChangesAsync(cancellationToken);

                return saveResult > 0
                    ? Result<Guid>.Success(branch.Id)
                    : Result<Guid>.Failure(SetupBranchErrors.DatabaseError);
            }
            catch (Exception)
            {
                return Result<Guid>.Failure(SetupBranchErrors.DatabaseError);
            }
        }
    }
}