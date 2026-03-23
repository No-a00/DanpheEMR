using FluentValidation;

namespace DanpheEMR.Application.Features.Appointments.Queries.GetDoctorSchedule
{
    public sealed class GetDoctorScheduleQueryValidator : AbstractValidator<GetDoctorScheduleQuery>
    {
        public GetDoctorScheduleQueryValidator()
        {
            RuleFor(x => x.DoctorId)
                .NotEmpty().WithMessage("Vui lòng chọn bác sĩ để xem lịch.");

            RuleFor(x => x.Date)
                .NotEmpty().WithMessage("Vui lòng chọn ngày cần xem lịch.");
        }
    }
}