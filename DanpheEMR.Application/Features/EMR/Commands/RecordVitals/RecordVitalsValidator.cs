using FluentValidation;

namespace DanpheEMR.Application.Features.EMR.Commands.RecordVitals
{
    public class RecordVitalsValidator : AbstractValidator<RecordVitalsCommand>
    {
        public RecordVitalsValidator()
        {
            RuleFor(x => x.PatientId)
                .NotEmpty().WithMessage("Mã bệnh nhân không được để trống.");

            RuleFor(x => x.VisitId)
                .NotEmpty().WithMessage("Mã lượt khám không được để trống.");

            RuleFor(x => x.HeartRate)
                .GreaterThan(0).WithMessage("Nhịp tim phải lớn hơn 0.")
                .LessThan(300).WithMessage("Nhịp tim vượt quá ngưỡng cho phép.");

            RuleFor(x => x.Temperature)
                .InclusiveBetween(30m, 45m).WithMessage("Nhiệt độ cơ thể phải nằm trong khoảng 30°C đến 45°C.");

            RuleFor(x => x.SpO2)
                .InclusiveBetween(0m, 100m).WithMessage("Chỉ số SpO2 phải từ 0% đến 100%.");

            RuleFor(x => x.Weight)
                .GreaterThan(0).WithMessage("Cân nặng phải lớn hơn 0.");

            RuleFor(x => x.Height)
                .GreaterThan(0).WithMessage("Chiều cao phải lớn hơn 0.");

            RuleFor(x => x.BloodPressure)
                .NotEmpty().WithMessage("Huyết áp không được để trống.")
                .Matches(@"^\d{2,3}\/\d{2,3}$").WithMessage("Huyết áp phải đúng định dạng (VD: 120/80).");
        }
    }
}S