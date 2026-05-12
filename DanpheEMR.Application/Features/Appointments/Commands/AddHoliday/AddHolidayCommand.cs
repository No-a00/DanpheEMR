
using MediatR;

namespace DanpheEMR.Features.Appointment.Commands.AddHoliday
{
    public record AddHolidayCommand(
        DateTime Date,
        string Reason,
        bool IsGlobal,
        string? ProviderCode // Có thể null nếu là ngày lễ toàn quốc
    ) : IRequest<Result<AddHolidayResponse>>;
}