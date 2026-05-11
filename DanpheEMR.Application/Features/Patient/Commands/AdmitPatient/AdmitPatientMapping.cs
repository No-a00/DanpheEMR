using AutoMapper;
using DanpheEMR.Core.Domain.Patients;
using DanpheEMR.Core.Enums;

namespace DanpheEMR.Application.Features.Patients.Commands.AdmitPatient
{
    public class AdmitPatientMapping : Profile
    {
        public AdmitPatientMapping()
        {
            CreateMap<AdmitPatientCommand, Admission>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => Guid.NewGuid()))
                .ForMember(dest => dest.AdmissionDate, opt => opt.MapFrom(src => DateTime.Now))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => AdmissionStatus.Active))
                .ForMember(dest => dest.AdmissionNotes, opt => opt.MapFrom(src => src.InitialDiagnosis));
        }
    }
}