using FluentValidation;

namespace DanpheEMR.Application.Features.Appointments.Queries.GetDoctorDailySchedule
{
    public class GetDoctorDailyScheduleQueryValidator : AbstractValidator<GetDoctorDailyScheduleQuery>
    {
        public GetDoctorDailyScheduleQueryValidator()
        {
            RuleFor(x => x.DoctorId)
                .NotEmpty().WithMessage("Mã bác sĩ không được để trống.");

            RuleFor(x => x.Date)
                .NotEmpty().WithMessage("Ngày xem lịch không được để trống.");
        }
    }
}