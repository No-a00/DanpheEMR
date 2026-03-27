using FluentValidation;
using System;

namespace DanpheEMR.Application.Features.Appointments.Commands.RescheduleAppointment
{
    public class RescheduleAppointmentValidator : AbstractValidator<RescheduleAppointmentCommand>
    {
        public RescheduleAppointmentValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Mã lịch hẹn không được để trống.");

            RuleFor(x => x.NewAppointmentDate)
                .NotEmpty().WithMessage("Ngày hẹn mới không được để trống.")
                .GreaterThanOrEqualTo(DateTime.Today).WithMessage("Ngày hẹn mới không được là ngày trong quá khứ.");

            RuleFor(x => x.Reason)
                .NotEmpty().WithMessage("Vui lòng nhập lý do dời lịch hẹn.")
                .MaximumLength(500).WithMessage("Lý do không được vượt quá 500 ký tự.");
        }
    }
}