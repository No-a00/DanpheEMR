using FluentValidation;

namespace DanpheEMR.Application.Features.Admin.Queries.GetEmployees
{
    public class GetEmployeesQueryValidator : AbstractValidator<GetEmployeesQuery>
    {
        public GetEmployeesQueryValidator()
        {
            RuleFor(x => x.SearchTerm)
                .MaximumLength(100).WithMessage("Từ khóa tìm kiếm không quá 100 ký tự.");
        }
    }
}