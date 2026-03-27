using Application.Common;
using DanpheEMR.Core.Interface.Appointments;
using MediatR;


namespace DanpheEMR.Application.Features.Appointments.Queries.GetDoctorDailySchedule
{
    public class GetDoctorDailyScheduleQueryHandler : IRequestHandler<GetDoctorDailyScheduleQuery, Result<GetDoctorDailyScheduleResponse>>
    {
        private readonly IAppointmentRepository _appointmentRepository;

        public GetDoctorDailyScheduleQueryHandler(IAppointmentRepository appointmentRepository)
        {
            _appointmentRepository = appointmentRepository;
        }

        public async Task<Result<GetDoctorDailyScheduleResponse>> Handle(GetDoctorDailyScheduleQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var appointments = await _appointmentRepository.GetAppointmentsByDoctorAsync(request.DoctorId, request.Date.Date);
                var appointmentDtos = appointments
                    .OrderBy(a => a.AppointmentTime)
                    .ToDtoList();

                var response = new GetDoctorDailyScheduleResponse
                {
                    DoctorId = request.DoctorId,
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