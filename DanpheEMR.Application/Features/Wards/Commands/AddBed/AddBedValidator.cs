using FluentValidation;

namespace DanpheEMR.Application.Features.Inpatient.Commands.AddBed
{
    public class AddBedValidator : AbstractValidator<AddBedCommand>
    {
        public AddBedValidator()
        {
            RuleFor(x => x.BedNumber).NotEmpty().WithMessage("Số giường không được để trống.");
            RuleFor(x => x.BedCode).NotEmpty().WithMessage("Mã giường không được để trống.");
            RuleFor(x => x.WardId).NotEmpty().WithMessage("Vui lòng chọn Buồng bệnh trực thuộc.");
        }
    }
}