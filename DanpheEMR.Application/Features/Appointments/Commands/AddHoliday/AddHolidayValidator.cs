using FluentValidation;
using System;

namespace DanpheEMR.Features.Appointment.Commands.AddHoliday
{
    public class AddHolidayValidator : AbstractValidator<AddHolidayCommand>
    {
        public AddHolidayValidator()
        {
            RuleFor(x => x.Date)
                .NotEmpty().WithMessage("Ngày nghỉ không được để trống.")
                .GreaterThanOrEqualTo(DateTime.Today).WithMessage("Ngày nghỉ không thể là ngày trong quá khứ.");

            RuleFor(x => x.Reason)
                .NotEmpty().WithMessage("Lý do nghỉ không được để trống.")
                .MaximumLength(500).WithMessage("Lý do không được vượt quá 500 ký tự.");
            RuleFor(x => x.ProviderCode)
                .NotNull().When(x => !x.IsGlobal)
                .WithMessage(x => $"Nếu đây là ngày nghỉ cá nhân, bạn phải chọn bác sĩ {x.ProviderCode}.");
        }
    }
}