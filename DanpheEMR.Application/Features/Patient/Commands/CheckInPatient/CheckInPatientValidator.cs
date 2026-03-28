using FluentValidation;

namespace DanpheEMR.Application.Features.Patients.Commands.CheckInPatient
{
    public class CheckInPatientValidator : AbstractValidator<CheckInPatientCommand>
    {
        public CheckInPatientValidator()
        {
            RuleFor(x => x.PatientId).NotEmpty().WithMessage("Bệnh nhân không hợp lệ.");
            RuleFor(x => x.DepartmentId).NotEmpty().WithMessage("Vui lòng chọn Phòng/Khoa khám.");
            RuleFor(x => x.ChiefComplaint).NotEmpty().WithMessage("Vui lòng nhập lý do khám bệnh.");
        }
    }
}