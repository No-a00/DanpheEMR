namespace DanpheEMR.Application.Features.Billing.Commands.GenerateInvoice
{
    public static class GenerateInvoiceConstants
    {
        public const string StatusUnpaid = "Unpaid";
        public const string StatusPaid = "Paid";
        public const string StatusCancelled = "Cancelled";
        public const string StatusPartial = "PartiallyPaid";

        // Loại hóa đơn (nếu hệ thống có nhiều loại)
        public const string TypeOPD = "OutPatient";
        public const string TypeIPD = "InPatient";
    }
}