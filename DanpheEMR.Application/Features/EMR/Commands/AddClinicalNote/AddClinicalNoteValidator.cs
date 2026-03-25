using FluentValidation;

namespace DanpheEMR.Application.Features.EMR.Commands.AddClinicalNote
{
    public class AddClinicalNoteValidator : AbstractValidator<AddClinicalNoteCommand>
    {
        public AddClinicalNoteValidator()
        {
            RuleFor(x => x.VisitId)
                .NotEmpty().WithMessage("Mã lịch hẹn (VisitId) không được để trống.");

            RuleFor(x => x.PatientId)
                .NotEmpty().WithMessage("Mã bệnh nhân (PatientId) không được để trống.");

            RuleFor(x => x.ProviderId)
                .NotEmpty().WithMessage("Mã bác sĩ (ProviderId) không được để trống.");
            RuleFor(x => x.ChiefComplaint)
                .NotEmpty().WithMessage("Triệu chứng chính không được để trống.")
                .MaximumLength(500).WithMessage("Triệu chứng chính không được vượt quá 500 ký tự.");

            RuleFor(x => x.HistoryOfPresentIllness)
                .MaximumLength(1000).WithMessage("Lịch sử bệnh hiện tại không được vượt quá 1000 ký tự.");

            RuleFor(x => x.ExaminationNotes)
                .MaximumLength(2000).WithMessage("Ghi chú khám bệnh không được vượt quá 2000 ký tự.");
        }
    }
}