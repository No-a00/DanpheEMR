using AutoMapper;

using DanpheEMR.Core.Interfaces.Appointment;
using MediatR;


namespace DanpheEMR.Application.Features.Appointments.Queries.GetDoctorSchedule
{
    public sealed class GetDoctorScheduleQueryHandler
        : IRequestHandler<GetDoctorScheduleQuery, Result<List<DoctorScheduleResponse>>>
    {
        private readonly IDoctorScheduleRepository _scheduleRepo;
        private readonly IMapper _mapper;

        public GetDoctorScheduleQueryHandler(IDoctorScheduleRepository scheduleRepo, IMapper mapper)
        {
            _scheduleRepo = scheduleRepo;
            _mapper = mapper;
        }

        public async Task<Result<List<DoctorScheduleResponse>>> Handle(
            GetDoctorScheduleQuery request,
            CancellationToken cancellationToken)
        {
            var schedules = await _scheduleRepo.GetSchedulesByProviderIdAsync(request.DoctorId, request.Date);
            if (schedules == null)
            {
                return Result.Success(new List<DoctorScheduleResponse>());
            }
            var response = _mapper.Map<List<DoctorScheduleResponse>>(schedules);

            return Result.Success(response);
        }
    }
}