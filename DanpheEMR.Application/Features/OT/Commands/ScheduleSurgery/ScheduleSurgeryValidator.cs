using FluentValidation;
using System;

namespace DanpheEMR.Application.Features.OT.Commands.ScheduleSurgery
{
    public class ScheduleSurgeryValidator : AbstractValidator<ScheduleSurgeryCommand>
    {
        public ScheduleSurgeryValidator()
        {
            RuleFor(x => x.PatientId).NotEmpty().WithMessage("Vui lòng chọn bệnh nhân.");
            RuleFor(x => x.OTRoomId).NotEmpty().WithMessage("Vui lòng chọn phòng mổ.");
            RuleFor(x => x.SurgeonId).NotEmpty().WithMessage("Vui lòng chỉ định Bác sĩ phẫu thuật (Surgeon).");

            RuleFor(x => x.SurgeryDate)
                .NotEmpty().WithMessage("Ngày phẫu thuật không được để trống.")
                .GreaterThanOrEqualTo(DateTime.Today).WithMessage("Ngày phẫu thuật không được nằm trong quá khứ.");

            RuleFor(x => x.StartTime).NotEmpty().WithMessage("Vui lòng nhập giờ bắt đầu.");

            RuleFor(x => x.EndTime)
                .NotEmpty().WithMessage("Vui lòng nhập giờ kết thúc.")
                .GreaterThan(x => x.StartTime).WithMessage("Giờ kết thúc phải diễn ra sau giờ bắt đầu!");

            RuleFor(x => x.SurgeryType).NotEmpty().WithMessage("Vui lòng nhập loại phẫu thuật.");
        }
    }
}