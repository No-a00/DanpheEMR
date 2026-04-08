using AutoMapper;
using DanpheEMR.Core.Domain.Admin;

namespace DanpheEMR.Application.Features.Admin.Commands.SetupDepartment
{
    public class SetupDepartmentMapping : Profile
    {
        public SetupDepartmentMapping()
        {
            CreateMap<SetupDepartmentCommand, Department>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => Guid.NewGuid()))
                .ForMember(dest => dest.SubDepartments, opt => opt.Ignore())
                .ForMember(dest => dest.Employees, opt => opt.Ignore())
                .ForMember(dest => dest.Visits, opt => opt.Ignore());
        }
    }
}