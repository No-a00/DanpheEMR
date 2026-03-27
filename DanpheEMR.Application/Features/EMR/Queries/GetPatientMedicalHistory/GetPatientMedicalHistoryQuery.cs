using Application.Common;
using MediatR;
using System;

namespace DanpheEMR.Application.Features.EMR.Queries.GetPatientMedicalHistory
{
    public record GetPatientMedicalHistoryQuery(
        Guid PatientId
    ) : IRequest<Result<GetPatientMedicalHistoryResponse>>;
}