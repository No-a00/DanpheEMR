using Application.Common;
using MediatR;
using System;

namespace DanpheEMR.Application.Features.Patients.Commands.AdmitPatient
{
    public record AdmitPatientCommand(
        Guid PatientId,
        Guid AdmittingDoctorId, 
        Guid DepartmentId,      
        Guid? WardId,          
        Guid? BedId,           
        string InitialDiagnosis 
    ) : IRequest<Result<Guid>>; 
}