using Application.Common;
using MediatR;
using System;
using System.Collections.Generic;

namespace DanpheEMR.Application.Features.OT.Queries.GetDailySurgerySchedule
{

    public record GetDailySurgeryScheduleQuery(DateTime? Date = null) : IRequest<Result<List<GetDailySurgeryScheduleResponse>>>;
}