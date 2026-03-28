
using AutoMapper;
using DanpheEMR.Core.Interface.Admin;
using MediatR;

namespace DanpheEMR.Application.Features.Admin.Queries.GetDepartmentTree
{
    public class GetDepartmentTreeQueryHandler : IRequestHandler<GetDepartmentTreeQuery, Result<List<GetDepartmentTreeResponse>>>
    {
        private readonly IDepartmentRepository _departmentRepository;
        private readonly IMapper _mapper;

        public GetDepartmentTreeQueryHandler(IDepartmentRepository departmentRepository, IMapper mapper)
        {
            _departmentRepository = departmentRepository;
            _mapper = mapper;
        }

        public async Task<Result<List<GetDepartmentTreeResponse>>> Handle(GetDepartmentTreeQuery request, CancellationToken cancellationToken)
        {
            
            var allDepartments = await _departmentRepository.GetAllAsync();

           
            var allDtos = _mapper.Map<List<GetDepartmentTreeResponse>>(allDepartments);

            
            var lookup = allDtos.ToLookup(d => d.ParentDepartmentId);

            foreach (var dto in allDtos)
            {
               
                dto.SubDepartments = lookup[dto.Id].ToList();
            }

         
            var rootDepartments = allDtos.Where(d => d.ParentDepartmentId == null).ToList();

            return Result<List<GetDepartmentTreeResponse>>.Success(rootDepartments);
        }
    }
}