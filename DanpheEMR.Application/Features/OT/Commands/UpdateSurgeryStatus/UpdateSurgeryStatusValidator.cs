using DanpheEMR.Core.Enums;
using FluentValidation;

namespace DanpheEMR.Application.Features.OT.Commands.UpdateSurgeryStatus
{
    public class UpdateSurgeryStatusValidator : AbstractValidator<UpdateSurgeryStatusCommand>
    {
        public UpdateSurgeryStatusValidator()
        {
            RuleFor(x => x.ScheduleId).NotEmpty();
            RuleFor(x => x.Status).IsInEnum().WithMessage("Trạng thái không hợp lệ.");

          
            RuleFor(x => x.CancelReason)
                .NotEmpty().When(x => x.Status == OTStatus.Cancelled)
                .WithMessage("Vui lòng nhập lý do hủy ca mổ.");
        }
    }
}