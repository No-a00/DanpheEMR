using FluentValidation;

namespace DanpheEMR.Application.Features.Admin.Commands.SetupBranch
{
    public class SetupBranchValidator : AbstractValidator<SetupBranchCommand>
    {
        public SetupBranchValidator()
        {
            RuleFor(x => x.BranchName)
                .NotEmpty().WithMessage("Tên chi nhánh không được để trống.")
                .MaximumLength(150).WithMessage("Tên chi nhánh không được vượt quá 150 ký tự.");

            RuleFor(x => x.Address)
                .NotEmpty().WithMessage("Địa chỉ không được để trống.");

            RuleFor(x => x.ContactEmail)
                .NotEmpty().WithMessage("Email liên hệ không được để trống.")
                .EmailAddress().WithMessage("Định dạng email không hợp lệ (Ví dụ đúng: abc@gmail.com).");
        }
    }
}