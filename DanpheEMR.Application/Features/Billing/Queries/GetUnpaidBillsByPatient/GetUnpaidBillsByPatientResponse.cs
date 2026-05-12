

namespace DanpheEMR.Application.Features.Billing.Queries.GetUnpaidBillsByPatient
{
    public class GetUnpaidBillsByPatientResponse
    {
        public string PatientCode { get; set; }
        public int TotalUnpaidBills { get; set; }
        public decimal TotalUnpaidAmount { get; set; } 
        public List<UnpaidBillDto> UnpaidBills { get; set; } = new();
    }

    public class UnpaidBillDto
    {
        public string TransactionCode { get; set; }
        public string InvoiceNumber { get; set; }
        public DateTime TransactionDate { get; set; }
        public decimal TotalAmount { get; set; }
    }
}