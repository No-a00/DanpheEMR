using AutoMapper;
namespace DanpheEMR.Application.Features.Appointments.Commands.BookAppointment;

using DanpheEMR.Core.Domain.Appointments;

public sealed class BookAppointmentMapping : Profile
{
    public BookAppointmentMapping()
    {
        CreateMap<BookAppointmentCommand, Appointment>()
            // Bỏ qua (Ignore) việc map cột Id, để EF Core và Database tự lo liệu
            .ForMember(dest => dest.Id, opt => opt.Ignore())

            // (Tùy chọn) Bỏ qua các cột hệ thống mà Command không bao giờ truyền vào
            // .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
            // .ForMember(dest => dest.Status, opt => opt.Ignore())
            ;
    }
}