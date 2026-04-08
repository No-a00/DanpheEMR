using AutoMapper;
using DanpheEMR.Core.Domain.Patients;
// using DanpheEMR.Application.Common.Models; // Nơi chứa DTO của bạn

namespace DanpheEMR.Application.Mappings
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            // 1. Ánh xạ cơ bản (Tên Property giống nhau tự động map)
            // CreateMap<Patient, PatientDto>().ReverseMap();

            // 2. Ánh xạ phức tạp (Tùy chỉnh các trường khác tên hoặc cần tính toán)
            /*
            CreateMap<Patient, PatientDetailsDto>()
                .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => $"{src.FirstName} {src.LastName}"))
                .ForMember(dest => dest.Age, opt => opt.MapFrom(src => DateTime.Now.Year - src.DateOfBirth.Year));
            */

            // Tương tự cho Appointment, Billing, Prescription...
        }
    }
}