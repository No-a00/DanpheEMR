using Application.Common;
using DanpheEMR.Core.Domain.Admin;
using DanpheEMR.Core.Interface.Appointments;
using DanpheEMR.Core.Interface.Base;
using MediatR;


namespace DanpheEMR.Application.Features.Appointments.Queries.GetDoctorDailySchedule
{
    public class GetDoctorDailyScheduleQueryHandler : IRequestHandler<GetDoctorDailyScheduleQuery, Result<GetDoctorDailyScheduleResponse>>
    {
        private readonly IAppointmentRepository _appointmentRepository;
        private readonly IGenericRepository<Employee> _employeeRepo;

        public GetDoctorDailyScheduleQueryHandler(IAppointmentRepository appointmentRepository,IGenericRepository<Employee> employeeRepo)
        {
            _appointmentRepository = appointmentRepository;
            _employeeRepo = employeeRepo;
        }

        public async Task<Result<GetDoctorDailyScheduleResponse>> Handle(GetDoctorDailyScheduleQuery request, CancellationToken cancellationToken)
        {
            try
            {

                var doctor = await _employeeRepo.GetFirstOrDefaultAsync(p => p.Code == request.DoctorCode);
                if (doctor == null) return Result<GetDoctorDailyScheduleResponse>.Failure(new Error("GetDoctorDailySchedule.NotFound", "không tồn tại nhân viên này!"));

                var appointments = await _appointmentRepository.GetAppointmentsByDoctorAsync(doctor.Id, request.Date.Date);
                var appointmentDtos = appointments
                    .OrderBy(a => a.AppointmentTime)
                    .ToDtoList();

                var response = new GetDoctorDailyScheduleResponse
                {
                    DoctorCode = request.DoctorCode,
                    Date = request.Date.Date,
                    TotalAppointments = appointmentDtos.Count,
                    Appointments = appointmentDtos
                };

                return Result<GetDoctorDailyScheduleResponse>.Success(response);
            }
            catch (Exception)
            {
                var error = new Error("GetDoctorDailySchedule.Error", "Đã xảy ra lỗi khi truy xuất lịch khám của Bác sĩ.");
                return Result<GetDoctorDailyScheduleResponse>.Failure(error);
            }
        }
    }
}