

using FluentValidation;

namespace DanpheEMR.Application.Features.EMR.Commands.AddProgressNote
{
    public class AddProgressNoteValidator : AbstractValidator<AddProgressNoteCommand>
    {
        public AddProgressNoteValidator() { 
            RuleFor(x=>x.AdmissionId).NotEmpty().WithMessage("AdmissionId không được để trống !");
            RuleFor(x=>x.ProviderId).NotEmpty().WithMessage("Bác sĩ chẩn đoán không được để trống.");
            RuleFor(x => x.Title).NotEmpty().WithMessage("Tiêu đề không được để trống.");
                RuleFor(x => x.Subjective)
                .NotEmpty().WithMessage("Chẩn đoán chủ quan không được để trống.")
                .MaximumLength(500).WithMessage("Chẩn đoán chủ quan không được vượt quá 500 ký tự.");
            RuleFor(x => x.Objective)
               .NotEmpty().WithMessage("Chẩn đoán khách quan không được để trống.")
               .MaximumLength(500).WithMessage("Chẩn đoán khách quan không được vượt quá 500 ký tự.");
            RuleFor(x => x.Assessment)
               .NotEmpty().WithMessage("Đánh giá không được để trống.")
               .MaximumLength(500).WithMessage("Đánh giá không được vượt quá 500 ký tự.");
            RuleFor(x => x.Plan)
               .NotEmpty().WithMessage("Kế hoạch không được để trống.")
               .MaximumLength(500).WithMessage("Kế hoạch không được vượt quá 500 ký tự.");

        }
    }
}
