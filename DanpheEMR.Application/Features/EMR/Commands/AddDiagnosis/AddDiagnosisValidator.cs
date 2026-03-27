using FluentValidation;

namespace DanpheEMR.Application.Features.EMR.Commands.AddDiagnosis
{
    public class AddDiagnosisValidator : AbstractValidator<AddDiagnosisCommand>
    {
        public AddDiagnosisValidator()
        {
            RuleFor(x => x.PatientId).NotEmpty().WithMessage("Bệnh nhân không được để trống.");
            RuleFor(x => x.VisitId).NotEmpty().WithMessage("Lượt khám không được để trống.");
            RuleFor(x => x.ProviderId).NotEmpty().WithMessage("Bác sĩ chẩn đoán không được để trống.");

            RuleFor(x => x.ICD10Code)
                .NotEmpty().WithMessage("Mã chẩn đoán ICD-10 không được để trống.")
                .MaximumLength(20).WithMessage("Mã ICD-10 quá dài.");

            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("Mô tả chẩn đoán không được để trống.")
                .MaximumLength(500).WithMessage("Mô tả không được vượt quá 500 ký tự.");
        }
    }
}