using FluentValidation;

namespace DanpheEMR.Application.Features.Inpatient.Queries.GetBedsByWard
{
    public class GetBedsByWardQueryValidator : AbstractValidator<GetBedsByWardQuery>
    {
        public GetBedsByWardQueryValidator()
        {
            RuleFor(x => x.WardId).NotEmpty().WithMessage("Vui lòng truyền ID của Buồng bệnh.");
        }
    }
}