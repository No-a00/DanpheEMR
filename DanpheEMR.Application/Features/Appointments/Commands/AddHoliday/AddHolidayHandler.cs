
using DanpheEMR.Application.Abstractions.Persistence;
using DanpheEMR.Core.Interfaces.Appointment;
using MediatR;
using Application.Common;
using DanpheEMR.Core.Interface.Base;
using DanpheEMR.Core.Domain.Admin;
namespace DanpheEMR.Features.Appointment.Commands.AddHoliday
{
    public class AddHolidayHandler : IRequestHandler<AddHolidayCommand, Result<AddHolidayResponse>>
    {
        private readonly IHolidayRepository _holidayRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IGenericRepository<Employee> _employeeRepository;

        public AddHolidayHandler(
            IHolidayRepository holidayRepository,
            IGenericRepository<Employee> employeeRepository,
            IUnitOfWork unitOfWork)
        {
            _holidayRepository = holidayRepository;
            _unitOfWork = unitOfWork;
            _employeeRepository = employeeRepository;
        }

        public async Task<Result<AddHolidayResponse>> Handle(AddHolidayCommand request, CancellationToken cancellationToken)
        {
            try
            {
                if (request.ProviderCode != null)
                {
                    var provider = await _employeeRepository.GetFirstOrDefaultAsync(p => p.Code == request.ProviderCode);
                    if (provider == null) return Result<AddHolidayResponse>.Failure(AddHolidayErrors.ProviderNotFound);
                }

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
            catch (Exception ex)
            {
                return Result<AddHolidayResponse>.Failure(new Error("AddHoliday.Exception",$"{ex.Message}"));
            }
        }
    }
}