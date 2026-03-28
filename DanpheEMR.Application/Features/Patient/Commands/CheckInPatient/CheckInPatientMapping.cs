using AutoMapper;
using DanpheEMR.Core.Domain.Patients;

namespace DanpheEMR.Application.Features.Patients.Commands.CheckInPatient
{
    public class CheckInPatientMapping : Profile
    {
        public CheckInPatientMapping()
        {
            CreateMap<CheckInPatientCommand, Visit>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => Guid.NewGuid()))
                .ForMember(dest => dest.VisitDate, opt => opt.MapFrom(src => DateTime.Now))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => "CheckedIn")); 
        }
    }
}