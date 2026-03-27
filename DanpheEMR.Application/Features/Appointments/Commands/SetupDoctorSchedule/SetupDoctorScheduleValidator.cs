using FluentValidation;
using System;

namespace DanpheEMR.Application.Features.Appointments.Commands.SetupDoctorSchedule
{
    public class SetupDoctorScheduleValidator : AbstractValidator<SetupDoctorScheduleCommand>
    {
        public SetupDoctorScheduleValidator()
        {
            RuleFor(x => x.ProviderId)
                .NotEmpty().WithMessage("Vui lòng chọn Bác sĩ.");

            RuleFor(x => x.DepartmentId)
                .NotEmpty().WithMessage("Vui lòng chọn Khoa phòng.");

            RuleFor(x => x.MaxPatients)
                .GreaterThan(0).WithMessage("Số lượng bệnh nhân tối đa cho một ca phải lớn hơn 0.")
                .LessThan(200).WithMessage("Số lượng bệnh nhân tối đa quá lớn, vui lòng kiểm tra lại.");

            // Kiểm tra giờ bắt đầu và kết thúc
            RuleFor(x => x.EndTime)
                .GreaterThan(x => x.StartTime)
                .WithMessage("Giờ kết thúc ca làm việc phải diễn ra sau giờ bắt đầu.");
            // Kiểm tra dữ liệu đầu vào phải là một Thứ hợp lệ (Từ 0 đến 6)
            RuleFor(x => x.DayOfWeek)
                .IsInEnum().WithMessage("Thứ trong tuần không hợp lệ (Phải từ Chủ nhật đến Thứ 7).");
        }
    }
}