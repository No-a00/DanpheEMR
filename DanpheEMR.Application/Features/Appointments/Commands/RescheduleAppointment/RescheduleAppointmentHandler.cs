
using DanpheEMR.Application.Abstractions.Persistence;
using DanpheEMR.Core.Enums;
using DanpheEMR.Core.Interface.Appointments;
using DanpheEMR.Core.Interfaces.Base;
using MediatR;


namespace DanpheEMR.Application.Features.Appointments.Commands.RescheduleAppointment
{
    public class RescheduleAppointmentHandler : IRequestHandler<RescheduleAppointmentCommand, Result<Guid>>
    {
        private readonly IAppointmentRepository _appointmentRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICurrentUserService _currentUserService;

        public RescheduleAppointmentHandler(
            IAppointmentRepository appointmentRepository,
            IUnitOfWork unitOfWork,
            ICurrentUserService currentUserService)
        {
            _appointmentRepository = appointmentRepository;
            _unitOfWork = unitOfWork;
            _currentUserService = currentUserService;
        }

        public async Task<Result<Guid>> Handle(RescheduleAppointmentCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var appointment = await _appointmentRepository.GetByIdAsync(request.Id);
                if (appointment == null)
                {
                    return Result<Guid>.Failure(RescheduleAppointmentErrors.NotFound);
                }

                if (!appointment.IsActive || appointment.Status == VisitStatus.Cancelled)
                {
                    return Result<Guid>.Failure(RescheduleAppointmentErrors.InvalidStatus);
                }


                bool isBusy = await _appointmentRepository.IsDoctorBusy(appointment.ProviderId, request.NewAppointmentDate);
                if (isBusy)
                {
                    return Result<Guid>.Failure(RescheduleAppointmentErrors.DoctorBusy);
                }

              
                request.UpdateEntity(appointment, _currentUserService.UserId);

                _appointmentRepository.Update(appointment);
                var saveResult = await _unitOfWork.SaveChangesAsync(cancellationToken);

                if (saveResult > 0)
                {
                    return Result<Guid>.Success(appointment.Id);
                }

                return Result<Guid>.Failure(RescheduleAppointmentErrors.DatabaseError);
            }
            catch (Exception)
            {
                return Result<Guid>.Failure(RescheduleAppointmentErrors.DatabaseError);
            }
        }
    }
}