using FluentValidation;

namespace DanpheEMR.Application.Features.Inpatient.Queries.GetWardOccupancy
{
    public class GetWardOccupancyQueryValidator : AbstractValidator<GetWardOccupancyQuery>
    {
        public GetWardOccupancyQueryValidator()
        {
            RuleFor(x => x.WardId).NotEmpty().WithMessage("Vui lòng truyền ID của Buồng bệnh.");
        }
    }
}