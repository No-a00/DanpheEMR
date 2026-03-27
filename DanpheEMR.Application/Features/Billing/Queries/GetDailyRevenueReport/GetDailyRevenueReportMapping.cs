using System.Collections.Generic;
using System.Linq;
using DanpheEMR.Core.Enums; // Nếu PaymentMode là Enum
// Tạo bí danh để tránh lỗi namespace với class Entity
using DomainBillingTransaction = DanpheEMR.Core.Domain.Billing.BillingTransaction;

namespace DanpheEMR.Application.Features.Billing.Queries.GetDailyRevenueReport
{
    public static class GetDailyRevenueReportMapping
    {
        public static RevenueTransactionItemDto ToDto(this DomainBillingTransaction transaction)
        {
            return new RevenueTransactionItemDto
            {
                TransactionId = transaction.Id,

                InvoiceNumber = transaction.InvoiceNumber,

                TransactionTime = transaction.TransactionDate.TimeOfDay,

                PatientName = transaction.Patient != null ? $"{transaction.Patient.FirstName} {transaction.Patient.LastName}" : "Khách vãng lai",

                Amount = transaction.TotalAmount,

                PaymentMethod = transaction.PaymentMode.ToString()
            };
        }

        public static List<RevenueTransactionItemDto> ToDtoList(this IEnumerable<DomainBillingTransaction> transactions)
        {
            return transactions.Select(t => t.ToDto()).ToList();
        }
    }
}