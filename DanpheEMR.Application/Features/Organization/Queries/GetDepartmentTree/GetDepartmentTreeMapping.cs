using AutoMapper;
using DanpheEMR.Core.Domain.Admin;

namespace DanpheEMR.Application.Features.Admin.Queries.GetDepartmentTree
{
    public class GetDepartmentTreeMapping : Profile
    {
        public GetDepartmentTreeMapping()
        {
            CreateMap<Department, GetDepartmentTreeResponse>();
        }
    }
}