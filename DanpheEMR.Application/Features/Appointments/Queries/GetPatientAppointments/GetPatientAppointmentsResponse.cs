using DanpheEMR.Core.Enums;

namespace DanpheEMR.Application.Features.Appointments.Queries.GetPatientAppointments
{
    public class GetPatientAppointmentsResponse
    {
        public Guid PatientId { get; set; }
        public int TotalAppointments { get; set; }
        public List<PatientAppointmentItemDto> Appointments { get; set; } = new();
    }

    public class PatientAppointmentItemDto
    {
        public Guid Id { get; set; }
        public DateTime AppointmentDate { get; set; }
        public TimeSpan AppointmentTime { get; set; }
        public string DoctorName { get; set; }
        public string DepartmentName { get; set; }
        public VisitStatus Status { get; set; }
        public string Reason { get; set; }
        public bool IsActive { get; set; }
    }
}