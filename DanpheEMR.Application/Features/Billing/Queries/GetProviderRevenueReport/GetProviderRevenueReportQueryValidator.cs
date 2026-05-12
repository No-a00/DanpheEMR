using FluentValidation;
using System;

namespace DanpheEMR.Application.Features.Billing.Queries.GetProviderRevenueReport
{
    public class GetProviderRevenueReportQueryValidator : AbstractValidator<GetProviderRevenueReportQuery>
    {
        public GetProviderRevenueReportQueryValidator()
        {
            RuleFor(x => x.ProviderCode)
                .NotEmpty().WithMessage("Mã bác sĩ không được để trống.");

            RuleFor(x => x.FromDate)
                .NotEmpty().WithMessage("Từ ngày không được để trống.");

            RuleFor(x => x.ToDate)
                .NotEmpty().WithMessage("Đến ngày không được để trống.")
                .GreaterThanOrEqualTo(x => x.FromDate)
                .WithMessage("Ngày kết thúc phải lớn hơn hoặc bằng ngày bắt đầu.");
        }
    }
}