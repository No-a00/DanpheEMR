using Application.Common;
using MediatR;
using System;

namespace DanpheEMR.Application.Features.Patients.Queries.GetPatientHistory
{
    public record GetPatientHistoryQuery(Guid PatientId) : IRequest<Result<PatientHistoryResponse>>;
}