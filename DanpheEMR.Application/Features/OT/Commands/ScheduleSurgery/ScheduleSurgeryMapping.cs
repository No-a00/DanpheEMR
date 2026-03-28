using AutoMapper;
using DanpheEMR.Core.Domain.OT;
using DanpheEMR.Core.Enums; 

namespace DanpheEMR.Application.Features.OT.Commands.ScheduleSurgery
{
    public class ScheduleSurgeryMapping : Profile
    {
        public ScheduleSurgeryMapping()
        {
            CreateMap<ScheduleSurgeryCommand, OTSchedule>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => Guid.NewGuid()))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => OTStatus.Scheduled)) 
                .ForMember(dest => dest.IsActive, opt => opt.MapFrom(src => true))
                .ForMember(dest => dest.patientId, opt => opt.MapFrom(src => src.PatientId))

                .ForMember(dest => dest.CancelReason, opt => opt.Ignore())
                .ForMember(dest => dest.CancelledByUserId, opt => opt.Ignore())
                .ForMember(dest => dest.Anesthetist, opt => opt.Ignore())
                .ForMember(dest => dest.Patient, opt => opt.Ignore())
                .ForMember(dest => dest.OTRoom, opt => opt.Ignore())
                .ForMember(dest => dest.Surgeon, opt => opt.Ignore());
        }
    }
}