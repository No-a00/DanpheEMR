
using DanpheEMR.Application.Abstractions.Persistence;
using DanpheEMR.Core.Interfaces.Appointment;
using MediatR;

namespace DanpheEMR.Features.Appointment.Commands.AddHoliday
{
    public class AddHolidayHandler : IRequestHandler<AddHolidayCommand, Result<AddHolidayResponse>>
    {
        private readonly IHolidayRepository _holidayRepository;
        private readonly IUnitOfWork _unitOfWork;

        public AddHolidayHandler(
            IHolidayRepository holidayRepository,
            IUnitOfWork unitOfWork)
        {
            _holidayRepository = holidayRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<AddHolidayResponse>> Handle(AddHolidayCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var holidayEntity = request.ToEntity();
                await _holidayRepository.AddAsync(holidayEntity);
                var saveResult = await _unitOfWork.SaveChangesAsync(cancellationToken);

                if (saveResult > 0)
                {
                    return Result<AddHolidayResponse>.Success(new AddHolidayResponse
                    {
                        Id = holidayEntity.Id,
                        Message = "Thêm ngày nghỉ thành công!"
                    });
                }

                return Result<AddHolidayResponse>.Failure(AddHolidayErrors.DatabaseError);
            }
            catch (Exception)
            {
                return Result<AddHolidayResponse>.Failure(AddHolidayErrors.DatabaseError);
            }
        }
    }
}