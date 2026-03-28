using Application.Common;
using MediatR;
using System.Collections.Generic;

namespace DanpheEMR.Application.Features.Patients.Queries.GetAdmittedPatients
{
    public record GetAdmittedPatientsQuery() : IRequest<Result<List<GetAdmittedPatientsResponse>>>;
}