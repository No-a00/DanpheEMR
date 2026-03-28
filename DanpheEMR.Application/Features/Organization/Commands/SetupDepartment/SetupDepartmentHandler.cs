
using AutoMapper;
using DanpheEMR.Core.Domain.Admin;
using DanpheEMR.Core.Interface;
using DanpheEMR.Core.Interface.Admin; 
using MediatR;


namespace DanpheEMR.Application.Features.Admin.Commands.SetupDepartment
{
    public class SetupDepartmentHandler : IRequestHandler<SetupDepartmentCommand, Result<Guid>>
    {
        private readonly IDepartmentRepository _departmentRepository;
        private readonly IEmployeeRepository _employeeRepository; // Để kiểm tra Trưởng khoa
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public SetupDepartmentHandler(
            IDepartmentRepository departmentRepository,
            IEmployeeRepository employeeRepository,
            IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _departmentRepository = departmentRepository;
            _employeeRepository = employeeRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<Guid>> Handle(SetupDepartmentCommand request, CancellationToken cancellationToken)
        {
            try
            {
                bool isCodeExists = await _departmentRepository.IsCodeExistsAsync(request.DepartmentCode);
                if (isCodeExists)
                {
                    return Result<Guid>.Failure(SetupDepartmentErrors.CodeExists);
                }
                if (request.ParentDepartmentId.HasValue)
                {
                    var parent = await _departmentRepository.GetByIdAsync(request.ParentDepartmentId.Value);
                    if (parent == null) return Result<Guid>.Failure(SetupDepartmentErrors.ParentNotFound);
                }
                if (request.HeadOfDepartmentId.HasValue)
                {
                    var head = await _employeeRepository.GetByIdAsync(request.HeadOfDepartmentId.Value);
                    if (head == null) return Result<Guid>.Failure(SetupDepartmentErrors.HeadNotFound);
                }
                var department = _mapper.Map<Department>(request);

                await _departmentRepository.AddAsync(department);
                var saveResult = await _unitOfWork.SaveChangesAsync(cancellationToken);

                return saveResult > 0
                    ? Result<Guid>.Success(department.Id)
                    : Result<Guid>.Failure(SetupDepartmentErrors.DatabaseError);
            }
            catch (Exception)
            {
                return Result<Guid>.Failure(SetupDepartmentErrors.DatabaseError);
            }
        }
    }
}