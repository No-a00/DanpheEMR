using AutoMapper;
using DanpheEMR.Core.Domain.Admin;
using System;

namespace DanpheEMR.Application.Features.Admin.Commands.RegisterEmployee
{
    public class RegisterEmployeeMapping : Profile
    {
        public RegisterEmployeeMapping()
        {
            CreateMap<RegisterEmployeeCommand, Employee>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => Guid.NewGuid()))
                .ForMember(dest => dest.IsActive, opt => opt.MapFrom(src => true))
                .ForMember(dest=>dest.Code,opt=>opt.Ignore())

                .ForMember(dest => dest.IsDeleted, opt => opt.Ignore())
                .ForMember(dest => dest.Department, opt => opt.Ignore())
                .ForMember(dest => dest.Users, opt => opt.Ignore())
                .ForMember(dest => dest.Schedules, opt => opt.Ignore());
        }
    }
}