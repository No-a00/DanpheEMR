
using AutoMapper;
using DanpheEMR.Core.Domain.Admin;
using DanpheEMR.Core.Interface;
using DanpheEMR.Core.Interface.Admin; 
using MediatR;

namespace DanpheEMR.Application.Features.Admin.Commands.RegisterEmployee
{
    public class RegisterEmployeeHandler : IRequestHandler<RegisterEmployeeCommand, Result<Guid>>
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IDepartmentRepository _departmentRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public RegisterEmployeeHandler(
            IEmployeeRepository employeeRepository,
            IDepartmentRepository departmentRepository,
            IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _employeeRepository = employeeRepository;
            _departmentRepository = departmentRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<Guid>> Handle(RegisterEmployeeCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var departmentExists = await _departmentRepository.GetByIdAsync(request.DepartmentId);
                if (departmentExists == null)
                {
                    return Result<Guid>.Failure(RegisterEmployeeErrors.DepartmentNotFound);
                }

                var employee = _mapper.Map<Employee>(request);

                await _employeeRepository.AddAsync(employee);

                var saveResult = await _unitOfWork.SaveChangesAsync(cancellationToken);

                return saveResult > 0
                    ? Result<Guid>.Success(employee.Id)
                    : Result<Guid>.Failure(RegisterEmployeeErrors.DatabaseError);
            }
            catch (Exception)
            {
                return Result<Guid>.Failure(RegisterEmployeeErrors.DatabaseError);
            }
        }
    }
}