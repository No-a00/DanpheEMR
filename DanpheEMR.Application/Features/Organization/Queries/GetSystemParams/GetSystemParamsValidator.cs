using FluentValidation;

namespace DanpheEMR.Application.Features.Admin.Queries.GetSystemParams
{
    public class GetSystemParamsValidator : AbstractValidator<GetSystemParamsQuery>
    {
        public GetSystemParamsValidator()
        {
            RuleFor(x => x.SearchTerm)
                .MaximumLength(100).WithMessage("Từ khóa tìm kiếm không được vượt quá 100 ký tự.");
        }
    }
}