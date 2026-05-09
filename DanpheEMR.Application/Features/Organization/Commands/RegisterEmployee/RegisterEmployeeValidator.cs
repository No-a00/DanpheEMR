using FluentValidation;
using System;

namespace DanpheEMR.Application.Features.Admin.Commands.RegisterEmployee
{
    public class RegisterEmployeeValidator : AbstractValidator<RegisterEmployeeCommand>
    {
        public RegisterEmployeeValidator()
        {
            RuleFor(x => x.FirstName).NotEmpty().WithMessage("Tên không được để trống.");
            RuleFor(x => x.LastName).NotEmpty().WithMessage("Họ không được để trống.");
            RuleFor(x => x.Workforce).NotEmpty().WithMessage("Chức vụ không được để trống.");

            RuleFor(x => x.DOB)
                .NotEmpty().WithMessage("Ngày sinh không được để trống.")
                .LessThan(DateTime.Today).WithMessage("Ngày sinh không hợp lệ (Phải là ngày trong quá khứ).");

            RuleFor(x => x.Gender).NotEmpty().WithMessage("Giới tính không được để trống.");

            RuleFor(x => x.ContactNumber)
                .NotEmpty().WithMessage("Số điện thoại không được để trống.")
                .MaximumLength(20).WithMessage("Số điện thoại không được vượt quá 20 ký tự.");

        }
    }
}