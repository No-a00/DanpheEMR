using Application.Common;
using MediatR;
using System;

namespace DanpheEMR.Application.Features.Billing.Queries.GetUnpaidBillsByPatient
{
    public record GetUnpaidBillsByPatientQuery(
        Guid PatientId
    ) : IRequest<Result<GetUnpaidBillsByPatientResponse>>;
}