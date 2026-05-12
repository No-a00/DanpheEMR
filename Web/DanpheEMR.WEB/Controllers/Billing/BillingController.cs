using DanpheEMR.Application.Features.Billing.Commands.GenerateInvoice;
using DanpheEMR.Application.Features.Billing.Commands.ProcessPayment;
using DanpheEMR.Application.Features.Billing.Queries.GetDailyRevenueReport;
using DanpheEMR.Application.Features.Billing.Queries.GetProviderRevenueReport;
using DanpheEMR.Application.Features.Billing.Queries.GetTransactionDetails;
using DanpheEMR.Application.Features.Billing.Queries.GetUnpaidBillsByPatient;
using DanpheEMR.WEB.Security;
using Microsoft.AspNetCore.Mvc;


namespace DanpheEMR.WEB.Controllers
{
    [Route("api/billing")]
    public class BillingController : ApiControllerBase
    {
        // POST: api/billing/invoices
        [HttpPost("invoices")]
        [RequirePermission("Billing", "Write")] 
        public async Task<IActionResult> GenerateInvoice([FromBody] GenerateInvoiceCommand command)
        {
            var result = await Mediator.Send(command);
            return Ok(result);
        }

        // POST: api/billing/payments
        [HttpPost("payments")]
        [RequirePermission("Billing", "Write")] 
        public async Task<IActionResult> ProcessPayment([FromBody] ProcessPaymentCommand command)
        {
            var result = await Mediator.Send(command);
            return Ok(result);
        }

        // GET: api/billing/transactions/{id}
        [HttpGet("transactions/{id}")]
        [RequirePermission("Billing", "Read")] 
        public async Task<IActionResult> GetTransactionDetails(string Code)
        {
            var result = await Mediator.Send(new GetTransactionDetailsQuery(Code));
            return Ok(result);
        }

        // GET: api/billing/patients/{patientId}/unpaid-bills
        [HttpGet("patients/{patientId}/unpaid-bills")]
        [RequirePermission("Billing", "Read")] 
        public async Task<IActionResult> GetUnpaidBillsByPatient(string patientCode)
        {
            var result = await Mediator.Send(new GetUnpaidBillsByPatientQuery(patientCode));
            return Ok(result);
        }

        // GET: api/billing/reports/daily-revenue?date=2023-10-25
        [HttpGet("reports/daily-revenue")]
        [RequirePermission("Billing", "Read")] 
        public async Task<IActionResult> GetDailyRevenueReport([FromQuery] DateTime date)
        {
            var result = await Mediator.Send(new GetDailyRevenueReportQuery(date));
            return Ok(result);
        }

        // GET: api/billing/reports/provider-revenue?providerId=...&startDate=...&endDate=...
        [HttpGet("reports/provider-revenue")]
        [RequirePermission("Billing", "Read")] 
        public async Task<IActionResult> GetProviderRevenueReport([FromQuery] GetProviderRevenueReportQuery query)
        {
            var result = await Mediator.Send(query);
            return Ok(result);
        }
    }
}