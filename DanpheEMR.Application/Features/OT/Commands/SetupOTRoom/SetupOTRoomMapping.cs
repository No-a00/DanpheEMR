using AutoMapper;
using DanpheEMR.Core.Domain.OT;
using System;

namespace DanpheEMR.Application.Features.OT.Commands.SetupOTRoom
{
    public class SetupOTRoomMapping : Profile
    {
        public SetupOTRoomMapping()
        {
            CreateMap<SetupOTRoomCommand, OTRoom>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => Guid.NewGuid()));
        }
    }
}