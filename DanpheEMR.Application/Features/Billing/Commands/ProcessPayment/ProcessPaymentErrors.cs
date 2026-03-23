using Application.Common;

namespace DanpheEMR.Application.Features.Billing.Commands.ProcessPayment
{
    public static class ProcessPaymentErrors
    {
        public static readonly Error InvoiceNotFound =
            new("Payment.InvoiceNotFound", "Không tìm thấy hóa đơn cần thanh toán.");

        public static readonly Error AlreadyPaid =
            new("Payment.AlreadyPaid", "Hóa đơn này đã được thanh toán xong.");

        public static readonly Error InvalidAmount =
            new("Payment.InvalidAmount", "Số tiền thanh toán không hợp lệ hoặc lớn hơn dư nợ.");

        public static readonly Error PaymentMethodNotSupported =
            new("Payment.MethodInvalid", "Phương thức thanh toán không được hỗ trợ.");
    }
}