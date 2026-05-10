using Application.Common;
using MediatR;
using System;

namespace DanpheEMR.Application.Features.Patients.Commands.CheckInPatient
{
    public record CheckInPatientCommand(
        string PatientCode,
        string DepartmentCode, // Bệnh nhân đăng ký khám khoa nào?
        string? DoctorCode,    // Có thể chọn đích danh bác sĩ hoặc không
        string ChiefComplaint // Lý do đến khám (VD: Đau bụng, Sốt cao)
    ) : IRequest<Result<string>>; // Trả về VisitCode
}