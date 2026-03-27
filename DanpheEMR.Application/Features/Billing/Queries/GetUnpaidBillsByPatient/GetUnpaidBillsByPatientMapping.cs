
using DomainBillingTransaction = DanpheEMR.Core.Domain.Billing.BillingTransaction;

namespace DanpheEMR.Application.Features.Billing.Queries.GetUnpaidBillsByPatient
{
    public static class GetUnpaidBillsByPatientMapping
    {
        public static UnpaidBillDto ToDto(this DomainBillingTransaction transaction)
        {
            return new UnpaidBillDto
            {
                TransactionId = transaction.Id,
                InvoiceNumber = transaction.InvoiceNumber,
                TransactionDate = transaction.TransactionDate,
                TotalAmount = transaction.TotalAmount
            };
        }

        public static List<UnpaidBillDto> ToDtoList(this IEnumerable<DomainBillingTransaction> transactions)
        {
            return transactions.Select(t => t.ToDto()).ToList();
        }
    }
}