using Application.Common;
using DanpheEMR.Core.Interface;
using DanpheEMR.Core.Interface.Appointments; // Chứa IDoctorScheduleRepository
using DanpheEMR.Core.Interfaces.Appointment;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace DanpheEMR.Application.Features.Appointments.Commands.SetupDoctorSchedule
{
    public class SetupDoctorScheduleHandler : IRequestHandler<SetupDoctorScheduleCommand, Result<Guid>>
    {
        private readonly IDoctorScheduleRepository _scheduleRepository;
        private readonly IUnitOfWork _unitOfWork;

        public SetupDoctorScheduleHandler(
            IDoctorScheduleRepository scheduleRepository,
            IUnitOfWork unitOfWork)
        {
            _scheduleRepository = scheduleRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<Guid>> Handle(SetupDoctorScheduleCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var scheduleEntity = request.ToEntity();

                
                await _scheduleRepository.AddAsync(scheduleEntity);

                
                var saveResult = await _unitOfWork.SaveChangesAsync(cancellationToken);

                if (saveResult > 0)
                {
                    return Result<Guid>.Success(scheduleEntity.Id);
                }

                return Result<Guid>.Failure(SetupDoctorScheduleErrors.DatabaseError);
            }
            catch (Exception)
            {
                return Result<Guid>.Failure(SetupDoctorScheduleErrors.DatabaseError);
            }
        }
    }
}