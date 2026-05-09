
using AutoMapper;
using DanpheEMR.Application.Abstractions.Persistence;
using DanpheEMR.Core.Domain.Admin;
using Application.Common;
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
                var departmentExists = await _departmentRepository.GetByCodeAsync(request.DepartmentCode);
                if (departmentExists == null)
                {
                    return Result<Guid>.Failure(RegisterEmployeeErrors.DepartmentNotFound);
                }

                var employee = _mapper.Map<Employee>(request);
                employee.DepartmentId = departmentExists.Id;
                employee.Code = await _employeeRepository.GenerateEmployeeCodeAsync(request.Workforce);

                await _employeeRepository.AddAsync(employee);

                var saveResult = await _unitOfWork.SaveChangesAsync(cancellationToken);

                return saveResult > 0
                    ? Result<Guid>.Success(employee.Id)
                    : Result<Guid>.Failure(RegisterEmployeeErrors.DatabaseError);
            }
            catch (Exception ex)
            {
                // Lấy lỗi sâu nhất từ Entity Framework để biết chính xác cột nào đang lỗi
                string errorMessage = ex.InnerException != null ? ex.InnerException.Message : ex.Message;

                // Tạm thời trả về thông báo lỗi chi tiết này ra ngoài Swagger
                // Lưu ý: Đảm bảo class Error của bạn có constructor nhận 2 tham số (Code, Message)
                return Result<Guid>.Failure(new Error("Debug.DBError", $"Chi tiết lỗi DB: {errorMessage}"));
            }
        }
    }
}