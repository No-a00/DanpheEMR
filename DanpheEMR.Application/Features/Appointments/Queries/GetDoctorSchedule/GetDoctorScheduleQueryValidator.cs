using FluentValidation;

namespace DanpheEMR.Application.Features.Appointments.Queries.GetDoctorSchedule
{
    public sealed class GetDoctorScheduleQueryValidator : AbstractValidator<GetDoctorScheduleQuery>
    {
        public GetDoctorScheduleQueryValidator()
        {
            RuleFor(x => x.DoctorCode)
                .NotEmpty().WithMessage("Vui lòng chọn bác sĩ để xem lịch.");

            RuleFor(x => x.StartDate)
                .NotEmpty().WithMessage("Vui lòng chọn ngày bắt đầu.");
            RuleFor(x => x.EndDate)
                .NotEmpty().WithMessage("Vui lòng chọn ngày kết thúc.");
        }
    }
}