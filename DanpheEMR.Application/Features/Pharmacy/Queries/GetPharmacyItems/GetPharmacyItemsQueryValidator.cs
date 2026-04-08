using FluentValidation;

namespace DanpheEMR.Application.Features.Pharmacy.Queries.GetPharmacyItems
{
    public class GetPharmacyItemsQueryValidator : AbstractValidator<GetPharmacyItemsQuery>
    {
        public GetPharmacyItemsQueryValidator()
        {
            RuleFor(x => x.SearchTerm)
                .MaximumLength(100).WithMessage("Từ khóa tìm kiếm không được vượt quá 100 ký tự.");
        }
    }
}