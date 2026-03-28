using AutoMapper;
using DanpheEMR.Core.Domain.Admin;

namespace DanpheEMR.Application.Features.Admin.Queries.GetSystemParams
{
    public class GetSystemParamsMapping : Profile
    {
        public GetSystemParamsMapping()
        {
            CreateMap<SystemParameter, GetSystemParamsResponse>();
        }
    }
}