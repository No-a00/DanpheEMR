using Application.Common;
using AutoMapper;
using DanpheEMR.Core.Interface.Admin; // Chứa IEmployeeRepository
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

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
            var employees = await _employeeRepository.GetEmployeesWithDepartmentAsync();
            var query = employees.AsEnumerable();

            if (!string.IsNullOrWhiteSpace(request.SearchTerm))
            {
                var search = request.SearchTerm.ToLower();
                query = query.Where(e => e.FullName.ToLower().Contains(search) ||
                                         e.ContactNumber.Contains(search));
            }

            if (request.DepartmentId.HasValue)
            {
                query = query.Where(e => e.DepartmentId == request.DepartmentId.Value);
            }

            var result = _mapper.Map<List<GetEmployeesResponse>>(query.ToList());

            return Result<List<GetEmployeesResponse>>.Success(result);
        }
    }
}