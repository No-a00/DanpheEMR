using FluentValidation;
using System;

namespace DanpheEMR.Application.Features.Patients.Commands.UpdatePatientInfo
{
    public class UpdatePatientInfoValidator : AbstractValidator<UpdatePatientInfoCommand>
    {
        public UpdatePatientInfoValidator()
        {
            RuleFor(x => x.PatientId).NotEmpty();
            RuleFor(x => x.FirstName).NotEmpty().WithMessage("Tên không được để trống.");
            RuleFor(x => x.LastName).NotEmpty().WithMessage("Họ không được để trống.");
            RuleFor(x => x.DOB).LessThan(DateTime.Today).WithMessage("Ngày sinh không hợp lệ.");
            RuleFor(x => x.PhoneNumber).Matches(@"^\d{10,11}$").WithMessage("SĐT phải từ 10-11 số.");
        }
    }
}