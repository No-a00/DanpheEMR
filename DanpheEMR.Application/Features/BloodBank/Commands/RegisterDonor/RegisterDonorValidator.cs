using System;
using FluentValidation;

namespace DanpheEMR.Application.Features.BloodBank.Commands.RegisterDonor
{
    public class RegisterDonorValidator : AbstractValidator<RegisterDonorCommand>
    {
        public RegisterDonorValidator()
        {
            RuleFor(x => x.DonorName)
                .NotEmpty().WithMessage("Tên người hiến máu không được để trống.")
                .MaximumLength(100).WithMessage("Tên không được vượt quá 100 ký tự.");

            RuleFor(x => x.Contact)
                .NotEmpty().WithMessage("Thông tin liên hệ không được để trống.")
                .MaximumLength(20).WithMessage("Liên hệ không được vượt quá 20 ký tự.")
                .Matches(@"^[0-9]+$").WithMessage("Số điện thoại chỉ được chứa các chữ số.");

            RuleFor(x => x.Gender)
                .NotEmpty().WithMessage("Giới tính không được để trống.")
                .MaximumLength(10).WithMessage("Giới tính không được vượt quá 10 ký tự.")
                .Must(g => g == "Male" || g == "Female" || g == "Other")
                .WithMessage("Giới tính phải là 'Male', 'Female', hoặc 'Other'.");

            RuleFor(x => x.BloodGroupId)
                .NotEmpty().WithMessage("Vui lòng chọn nhóm máu.");

            RuleFor(x => x.Weight)
                .GreaterThanOrEqualTo(45f).WithMessage("Cân nặng phải từ 45kg trở lên.");

            RuleFor(x => x.DateOfBirth)
                .NotEmpty().WithMessage("Ngày sinh không được để trống.")
                .Must(BeAValidAge).WithMessage("Người hiến máu phải nằm trong độ tuổi từ 18 đến 60.");
        }

        private bool BeAValidAge(DateTime dateOfBirth)
        {
            var today = DateTime.Today;
            var age = today.Year - dateOfBirth.Year;
            if (dateOfBirth.Date > today.AddYears(-age))
            {
                age--;
            }

            return age >= 18 && age <= 60;
        }
    }
}