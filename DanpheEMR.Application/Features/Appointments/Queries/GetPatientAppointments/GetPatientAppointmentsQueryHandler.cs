using Application.Common;
using DanpheEMR.Core.Interface.Appointments; 
using MediatR;

namespace DanpheEMR.Application.Features.Appointments.Queries.GetPatientAppointments
{
    public class GetPatientAppointmentsQueryHandler : IRequestHandler<GetPatientAppointmentsQuery, Result<GetPatientAppointmentsResponse>>
    {
        private readonly IAppointmentRepository _appointmentRepository;

        public GetPatientAppointmentsQueryHandler(IAppointmentRepository appointmentRepository)
        {
            _appointmentRepository = appointmentRepository;
        }

        public async Task<Result<GetPatientAppointmentsResponse>> Handle(GetPatientAppointmentsQuery request, CancellationToken cancellationToken)
        {
            try
            {
                
                var appointments = await _appointmentRepository.GetAppointmentsByPatientAsync(request.PatientId);

                
                var appointmentDtos = appointments
                    .OrderByDescending(a => a.AppointmentDate)
                    .ThenByDescending(a => a.AppointmentTime)
                    .ToDtoList();

                
                var response = new GetPatientAppointmentsResponse
                {
                    PatientId = request.PatientId,
                    TotalAppointments = appointmentDtos.Count,
                    Appointments = appointmentDtos
                };

                return Result<GetPatientAppointmentsResponse>.Success(response);
            }
            catch (Exception)
            {
                var error = new Error("GetPatientAppointments.Error", "Đã xảy ra lỗi khi truy xuất lịch sử khám của bệnh nhân.");
                return Result<GetPatientAppointmentsResponse>.Failure(error);
            }
        }
    }
}