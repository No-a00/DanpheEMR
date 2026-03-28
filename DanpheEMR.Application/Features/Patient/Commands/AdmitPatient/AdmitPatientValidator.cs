using FluentValidation;

namespace DanpheEMR.Application.Features.Patients.Commands.AdmitPatient
{
    public class AdmitPatientValidator : AbstractValidator<AdmitPatientCommand>
    {
        public AdmitPatientValidator()
        {
            RuleFor(x => x.PatientId).NotEmpty().WithMessage("Bệnh nhân không hợp lệ.");
            RuleFor(x => x.AdmittingDoctorId).NotEmpty().WithMessage("Vui lòng chọn Bác sĩ chỉ định nhập viện.");
            RuleFor(x => x.InitialDiagnosis).NotEmpty().WithMessage("Vui lòng nhập chẩn đoán ban đầu.");
        }
    }
}