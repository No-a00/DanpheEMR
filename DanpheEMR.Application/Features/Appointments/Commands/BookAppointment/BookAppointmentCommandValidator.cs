using DanpheEMR.Application.Features.Appointment.Commands.BookAppointment;
using FluentValidation;


// 1. Đã thêm DanpheEMR vào namespace
namespace DanpheEMR.Application.Features.Appointments.Commands.BookAppointment
{
    public sealed class BookAppointmentCommandValidator : AbstractValidator<BookAppointmentCommand>
    {
        public BookAppointmentCommandValidator()
        {
            RuleFor(x => x.PatientCode)
                .NotEmpty().WithMessage("Vui lòng chọn bệnh nhân."); // Thêm câu báo lỗi thân thiện

            RuleFor(x => x.DocTorCode)
                .NotEmpty().WithMessage("Vui lòng chọn bác sĩ.");

            RuleFor(x => x.AppointmentDate)
                .GreaterThan(DateTime.Now).WithMessage("Thời gian đặt lịch phải lớn hơn thời gian hiện tại.");

            RuleFor(x => x.Reason)
                // 2. Dùng Hằng số thay vì số 500
                .MaximumLength(BookAppointmentConstants.MaxReasonLength)
                .WithMessage($"Lý do khám không được vượt quá {BookAppointmentConstants.MaxReasonLength} ký tự.");
        }
    }
}