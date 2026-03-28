using Application.Common;
using MediatR;
using System;

namespace DanpheEMR.Application.Features.Patients.Commands.CheckInPatient
{
    public record CheckInPatientCommand(
        Guid PatientId,
        Guid DepartmentId, // Bệnh nhân đăng ký khám khoa nào?
        Guid? DoctorId,    // Có thể chọn đích danh bác sĩ hoặc không
        string ChiefComplaint // Lý do đến khám (VD: Đau bụng, Sốt cao)
    ) : IRequest<Result<Guid>>; // Trả về VisitId
}