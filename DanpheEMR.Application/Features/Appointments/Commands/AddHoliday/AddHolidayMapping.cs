using DanpheEMR.Core.Domain.Appointments;

namespace DanpheEMR.Features.Appointment.Commands.AddHoliday
{
    public static class AddHolidayMapping
    {
        public static Holiday ToEntity(this AddHolidayCommand command)
        {
            return new Holiday
            {
                Id = Guid.NewGuid(),
                Date = command.Date.Date, 
                Reason = command.Reason,
                IsGlobal = command.IsGlobal,
                
            };
        }
    }
}