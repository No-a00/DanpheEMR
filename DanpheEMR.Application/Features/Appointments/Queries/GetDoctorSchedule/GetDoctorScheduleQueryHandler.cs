using AutoMapper;
using Application.Common;
using DanpheEMR.Core.Domain.Admin;
using DanpheEMR.Core.Interface.Base;
using DanpheEMR.Core.Interfaces.Appointment;
using MediatR;


namespace DanpheEMR.Application.Features.Appointments.Queries.GetDoctorSchedule
{
    public sealed class GetDoctorScheduleQueryHandler
        : IRequestHandler<GetDoctorScheduleQuery, Result<List<DoctorScheduleResponse>>>
    {
        private readonly IDoctorScheduleRepository _scheduleRepo;
        private readonly IGenericRepository<Employee> _employeeRepo;
        private readonly IMapper _mapper;

        public GetDoctorScheduleQueryHandler(IDoctorScheduleRepository scheduleRepo, IGenericRepository<Employee> employeeRepo, Mapper mapper)
        {
            _scheduleRepo = scheduleRepo;
            _employeeRepo = employeeRepo;
            _mapper = mapper;
        }

        public async Task<Result<List<DoctorScheduleResponse>>> Handle(
            GetDoctorScheduleQuery request,
            CancellationToken cancellationToken)
        {
            try
            {
                var doctor = await _employeeRepo.GetFirstOrDefaultAsync(p => p.Code == request.DoctorCode);
                if (doctor == null) return Result<List<DoctorScheduleResponse>>.Failure(new Error("DoctorSchedule.NotFound", "không tìm thấy nhân viên này!"));
                var schedules = await _scheduleRepo.GetSchedulesByProviderIdAsync(doctor.Id, request.StartDate, request.EndDate);
                if (schedules == null)
                {
                    return Result.Success(new List<DoctorScheduleResponse>());
                }
                var response = _mapper.Map<List<DoctorScheduleResponse>>(schedules);

                return Result.Success(response);
            }
            catch (Exception ex)
            {

                return Result<List<DoctorScheduleResponse>>.Failure(new Error("DoctorSchedule.Exception", $"{ex.Message}"));
            }
        }
    }
}