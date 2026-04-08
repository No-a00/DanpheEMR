using AutoMapper;
using DanpheEMR.Core.Domain.Patients;
using DanpheEMR.Core.Enums;

namespace DanpheEMR.Application.Features.Patients.Commands.TransferPatient
{
    public class TransferPatientMapping : Profile
    {
        public TransferPatientMapping()
        {
            CreateMap<TransferPatientCommand, Transfer>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => Guid.NewGuid()))
                .ForMember(dest => dest.TransferDate, opt => opt.MapFrom(src => DateTime.Now))
                .ForMember(dest => dest.IsDeleted, opt => opt.MapFrom(src => false))
                .ForMember(dest => dest.TransferStatus, opt => opt.MapFrom(src => TransferStatus.Pending))
                .ForMember(dest => dest.Reason, opt => opt.Ignore())
                .ForMember(dest => dest.DeletedBy, opt => opt.Ignore());
        }
    }
}