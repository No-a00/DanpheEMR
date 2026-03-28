using AutoMapper;
using DanpheEMR.Core.Domain.Admin;
using System;

namespace DanpheEMR.Application.Features.Admin.Commands.SetupBranch
{
    public class SetupBranchMapping : Profile
    {
        public SetupBranchMapping()
        {
            CreateMap<SetupBranchCommand, Branch>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => Guid.NewGuid()));
        }
    }
}