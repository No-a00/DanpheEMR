using Application.Common;
using MediatR;
using System;

namespace DanpheEMR.Application.Features.Patients.Queries.GetPatientById
{
    public record GetPatientByIdQuery(Guid Id) : IRequest<Result<GetPatientResponse>>;
}