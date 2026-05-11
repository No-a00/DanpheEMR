using FluentValidation;

namespace DanpheEMR.Application.Features.Patients.Commands.AdmitPatient
{
    public class AdmitPatientValidator : AbstractValidator<AdmitPatientCommand>
    {
        public AdmitPatientValidator()
        {
            RuleFor(x => x.PatientCode).NotEmpty().WithMessage("Bệnh nhân không hợp lệ.");
            RuleFor(x => x.AdmittingDoctorCode).NotEmpty().WithMessage("Vui lòng chọn Bác sĩ chỉ định nhập viện.");
            RuleFor(x => x.InitialDiagnosis).NotEmpty().WithMessage("Vui lòng nhập chẩn đoán ban đầu.");
        }
    }
}