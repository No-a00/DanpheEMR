using AutoMapper;
using DanpheEMR.Core.Domain.Admin;

namespace DanpheEMR.Application.Features.Admin.Queries.GetEmployees
{
    public class GetEmployeesMapping : Profile
    {
        public GetEmployeesMapping()
        {
            CreateMap<Employee, GetEmployeesResponse>()
                .ForMember(dest => dest.DepartmentName, opt => opt.MapFrom(src => src.Department != null ? src.Department.DepartmentName : "Chưa xếp khoa"));
        }
    }
}