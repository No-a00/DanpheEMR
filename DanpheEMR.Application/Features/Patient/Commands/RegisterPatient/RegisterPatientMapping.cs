using AutoMapper;
using PatientModel = DanpheEMR.Core.Domain.Patients.Patient;

namespace DanpheEMR.Application.Features.Patients.Commands.RegisterPatient
{
    public class RegisterPatientMapping : Profile
    {
        public RegisterPatientMapping()
        {
            CreateMap<RegisterPatientCommand, PatientModel>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => Guid.NewGuid()))
                .ForMember(dest => dest.IsDeleted, opt => opt.MapFrom(src => false))
                .ForMember(dest => dest.PatientCode, opt => opt.Ignore())

                
                .ForMember(dest => dest.Addresses, opt => opt.Ignore())
                .ForMember(dest => dest.Kins, opt => opt.Ignore())
                .ForMember(dest => dest.Visits, opt => opt.Ignore())
                .ForMember(dest => dest.Admissions, opt => opt.Ignore())
                .ForMember(dest => dest.Reason, opt => opt.Ignore())
                .ForMember(dest => dest.DeletedBy, opt => opt.Ignore());
        }
    }
}