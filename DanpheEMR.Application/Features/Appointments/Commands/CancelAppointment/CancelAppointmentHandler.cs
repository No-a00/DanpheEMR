
using DanpheEMR.Application.Features.Appointments.Commands.CancelAppointment;
using DanpheEMR.Core.Interface;
using DanpheEMR.Core.Interface.Appointments; 
using DanpheEMR.Core.Interfaces.Base;
using MediatR;


namespace DanpheEMR.Application.Features.Appointment.Commands.CancelAppointment
{
    public class CancelAppointmentHandler : IRequestHandler<CancelAppointmentCommand, Result<Guid>>
    {
        private readonly IAppointmentRepository _appointmentRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICurrentUserService _currentUserService;

        public CancelAppointmentHandler(
            IAppointmentRepository appointmentRepository,
            IUnitOfWork unitOfWork,
            ICurrentUserService currentUserService)
        {
            _appointmentRepository = appointmentRepository;
            _unitOfWork = unitOfWork;
            _currentUserService = currentUserService;
        }

        public async Task<Result<Guid>> Handle(CancelAppointmentCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var appointment = await _appointmentRepository.GetByIdAsync(request.Id);
                if (appointment == null)
                {
                    return Result<Guid>.Failure(CancelAppointmentErrors.NotFound);
                }
                if (!appointment.IsActive)
                {
                    return Result<Guid>.Failure(CancelAppointmentErrors.AlreadyCanceled);
                }

               
                var userId = _currentUserService.UserId; 
                request.UpdateEntity(appointment, userId);

                
                 _appointmentRepository.Update(appointment);

                var saveResult = await _unitOfWork.SaveChangesAsync(cancellationToken);

                if (saveResult > 0)
                {
                    return Result<Guid>.Success(appointment.Id);
                }

                return Result<Guid>.Failure(CancelAppointmentErrors.DatabaseError);
            }
            catch (Exception)
            {
                return Result<Guid>.Failure(CancelAppointmentErrors.DatabaseError);
            }
        }
    }
}