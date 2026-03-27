using FluentValidation;

namespace DanpheEMR.Application.Features.Appointments.Commands.CancelAppointment
{
    public class CancelAppointmentValidator : AbstractValidator<CancelAppointmentCommand>
    {
        public CancelAppointmentValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Mã lịch hẹn không được để trống.");

            RuleFor(x => x.CancelReason)
                .NotEmpty().WithMessage("Vui lòng nhập lý do hủy lịch hẹn.")
                .MaximumLength(500).WithMessage("Lý do hủy không được vượt quá 500 ký tự.");
        }
    }
}