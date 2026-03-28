using FluentValidation;
using System;

namespace DanpheEMR.Application.Features.Patients.Commands.RegisterPatient
{
    public class RegisterPatientValidator : AbstractValidator<RegisterPatientCommand>
    {
        public RegisterPatientValidator()
        {
            RuleFor(x => x.FirstName).NotEmpty().WithMessage("Tên không được để trống.");
            RuleFor(x => x.LastName).NotEmpty().WithMessage("Họ không được để trống.");

            RuleFor(x => x.DOB)
                .NotEmpty().WithMessage("Ngày sinh không được để trống.")
                .LessThanOrEqualTo(DateTime.Today).WithMessage("Ngày sinh không hợp lệ.");

            RuleFor(x => x.Gender).NotEmpty().WithMessage("Vui lòng chọn giới tính.");

            RuleFor(x => x.PhoneNumber)
                .NotEmpty().WithMessage("Số điện thoại không được để trống.")
                .Matches(@"^\d{10,11}$").WithMessage("Số điện thoại phải từ 10-11 số.");

            // IdCardNumber có thể không bắt buộc với trẻ em, nhưng nếu nhập thì phải chuẩn
            RuleFor(x => x.IdCardNumber)
                .Matches(@"^\d{9,12}$").When(x => !string.IsNullOrEmpty(x.IdCardNumber))
                .WithMessage("CMND/CCCD phải từ 9 đến 12 số.");
        }
    }
}