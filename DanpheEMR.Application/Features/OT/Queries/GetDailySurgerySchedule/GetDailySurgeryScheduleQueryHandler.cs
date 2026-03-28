
using AutoMapper;
using DanpheEMR.Core.Interface.OT;
using MediatR;

namespace DanpheEMR.Application.Features.OT.Queries.GetDailySurgerySchedule
{
    public class GetDailySurgeryScheduleQueryHandler : IRequestHandler<GetDailySurgeryScheduleQuery, Result<List<GetDailySurgeryScheduleResponse>>>
    {
        private readonly IOTScheduleRepository _otScheduleRepository;
        private readonly IMapper _mapper;

        public GetDailySurgeryScheduleQueryHandler(IOTScheduleRepository otScheduleRepository, IMapper mapper)
        {
            _otScheduleRepository = otScheduleRepository;
            _mapper = mapper;
        }

        public async Task<Result<List<GetDailySurgeryScheduleResponse>>> Handle(GetDailySurgeryScheduleQuery request, CancellationToken cancellationToken)
        {
            var targetDate = request.Date ?? DateTime.Today;

            var schedules = await _otScheduleRepository.GetSchedulesByDateAsync(targetDate);
            var result = _mapper.Map<List<GetDailySurgeryScheduleResponse>>(schedules);

            return Result<List<GetDailySurgeryScheduleResponse>>.Success(result);
        }
    }
}