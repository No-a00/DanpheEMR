using Application.Common;
using MediatR;
using System;

namespace DanpheEMR.Application.Features.Patients.Commands.AdmitPatient
{
    public record AdmitPatientCommand(
        string PatientCode,
        string AdmittingDoctorCode, 
        string DepartmentCode,      
        string? WardCode,          
        string? BedCode,           
        string InitialDiagnosis 
    ) : IRequest<Result<Guid>>; 
}