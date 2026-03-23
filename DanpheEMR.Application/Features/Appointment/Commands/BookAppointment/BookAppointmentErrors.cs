using  Application.Common;

namespace Application.Features.Appointments.Commands.BookAppointment
{

    public static class BookAppointmentErrors
    {
        public static readonly Error DoctorNotFound =
            new(
                "Appointment.DoctorNotFound",
                "Doctor does not exist"
            );

        public static readonly Error PatientNotFound =
            new(
                "Appointment.PatientNotFound",
                "Patient does not exist"
            );

        public static readonly Error DoctorNotAvailable =
            new(
                "Appointment.DoctorNotAvailable",
                "Doctor is not available at selected time"
            );

        public static readonly Error HolidayConflict =
            new(
                "Appointment.HolidayConflict",
                "Appointment date falls on holiday"
            );

        public static readonly Error InvalidDate =
            new(
                "Appointment.InvalidDate",
                "Appointment date must be in the future"
            );

        public static readonly Error ScheduleConflict =
            new(
                "Appointment.ScheduleConflict",
                "Doctor already has appointment at this time"
            );
    }
}