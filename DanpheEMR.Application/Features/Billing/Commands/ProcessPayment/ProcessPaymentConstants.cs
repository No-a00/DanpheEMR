namespace DanpheEMR.Application.Features.Billing.Commands.ProcessPayment
{
    public static class ProcessPaymentConstants
    {
        // Phương thức thanh toán
        public const string MethodCash = "Cash";
        public const string MethodCard = "Card";
        public const string MethodTransfer = "BankTransfer";
        public const string MethodInsurance = "Insurance";

        // Trạng thái giao dịch
        public const string PaymentSuccess = "Success";
        public const string PaymentFailed = "Failed";
    }
}