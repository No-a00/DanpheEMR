using Application.Common;
using AutoMapper;
using DanpheEMR.Core.Interface.Admin;
using MediatR;

namespace DanpheEMR.Application.Features.Admin.Queries.GetEmployees
{
    public class GetEmployeesQueryHandler : IRequestHandler<GetEmployeesQuery, Result<List<GetEmployeesResponse>>>
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IMapper _mapper;

        public GetEmployeesQueryHandler(IEmployeeRepository employeeRepository, IMapper mapper)
        {
            _employeeRepository = employeeRepository;
            _mapper = mapper;
        }

        public async Task<Result<List<GetEmployeesResponse>>> Handle(GetEmployeesQuery request, CancellationToken cancellationToken)
        {
            try        
            {

                var employees = await _employeeRepository.GetEmployeesWithDepartmentAsync(
                    request.SearchTerm,
                    request.DepartmentCode
                );

                var result = _mapper.Map<List<GetEmployeesResponse>>(employees);

                

                return Result<List<GetEmployeesResponse>>.Success(result);

            }
            catch (Exception ex)
            {
                return Result<List<GetEmployeesResponse>>.Failure(new Error(
                    "lỗi khi lấy danh sách nhân viên",
                    $"An error occurred while retrieving employees: {ex.Message}"
                    ));
            }
        }
    }
}