using FluentValidation;

namespace DanpheEMR.Application.Features.EMR.Commands.CreatePrescription
{
    public class CreatePrescriptionValidator : AbstractValidator<CreatePrescriptionCommand>
    {
        public CreatePrescriptionValidator()
        {
            RuleFor(x => x.VisitId)
                .NotEmpty().WithMessage("Mã lịch hẹn (VisitId) không được để trống.");

            RuleFor(x => x.PatientId)
                .NotEmpty().WithMessage("Mã bệnh nhân (PatientId) không được để trống.");

            RuleFor(x => x.PrescriberId)
                .NotEmpty().WithMessage("Mã bác sĩ kê đơn (PrescriberId) không được để trống.");

            RuleFor(x => x.Notes)
                .MaximumLength(1000).WithMessage("Ghi chú không được vượt quá 1000 ký tự.");

            RuleFor(x => x.Items)
                .NotEmpty().WithMessage("Đơn thuốc phải có ít nhất một loại thuốc.")
                .Must(items => items != null && items.Count > 0).WithMessage("Danh sách thuốc không hợp lệ.");
        }
    }
}