using FluentValidation;

namespace DanpheEMR.Application.Features.Billing.Queries.GetDailyRevenueReport
{
    public class GetDailyRevenueReportQueryValidator : AbstractValidator<GetDailyRevenueReportQuery>
    {
        public GetDailyRevenueReportQueryValidator()
        {
            RuleFor(x => x.ReportDate)
                .NotEmpty().WithMessage("Ngày xem báo cáo không được để trống.");
        }
    }
}