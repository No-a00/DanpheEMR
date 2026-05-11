using Application.Common;
using MediatR;
using System;

namespace DanpheEMR.Application.Features.Inpatient.Commands.AddBed
{
    public record AddBedCommand(
        string BedNumber,
        string BedCode,
        string WardCode,
        string FeatureCode
    ) : IRequest<Result<Guid>>;
}