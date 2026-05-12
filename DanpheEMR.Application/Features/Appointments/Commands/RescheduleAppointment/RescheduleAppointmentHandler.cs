
using DanpheEMR.Application.Abstractions.Persistence;
using DanpheEMR.Core.Domain.Admin;
using DanpheEMR.Core.Enums;
using DanpheEMR.Core.Interface.Appointments;
using DanpheEMR.Core.Interface.Base;
using DanpheEMR.Core.Interfaces.Base;
using MediatR;


namespace DanpheEMR.Application.Features.Appointments.Commands.RescheduleAppointment
{
    public class RescheduleAppointmentHandler : IRequestHandler<RescheduleAppointmentCommand, Result<Guid>>
    {
        private readonly IAppointmentRepository _appointmentRepository;
        private readonly IGenericRepository<User> _userRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICurrentUserService _currentUserService;

        public RescheduleAppointmentHandler(
            IAppointmentRepository appointmentRepository,
            IGenericRepository<User> userRepository,
            IUnitOfWork unitOfWork,
            ICurrentUserService currentUserService)
        {
            _appointmentRepository = appointmentRepository;
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
            _currentUserService = currentUserService;
        }

        public async Task<Result<Guid>> Handle(RescheduleAppointmentCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var appointment = await _appointmentRepository.GetByCodeAsync(request.AppointmentCode);
                if (appointment == null)
                {
                    return Result<Guid>.Failure(RescheduleAppointmentErrors.NotFound);
                }

                if (!appointment.IsActive || appointment.Status == VisitStatus.Cancelled)
                {
                    return Result<Guid>.Failure(RescheduleAppointmentErrors.InvalidStatus);
                }


                bool isBusy = await _appointmentRepository.IsDoctorBusy(appointment.DoctorCode, request.NewAppointmentDate);
                if (isBusy)
                {
                    return Result<Guid>.Failure(RescheduleAppointmentErrors.DoctorBusy);
                }

              var userId = _currentUserService.UserId;
                var user = await _userRepository.GetFirstOrDefaultAsync(u => u.Id == userId);
                request.UpdateEntity(appointment, user.Code);

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