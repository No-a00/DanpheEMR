using Application.Common;
namespace DanpheEMR.Application.Features.Billing.Commands.GenerateInvoice
{
    public static class GenerateInvoiceErrors
    {
        public static readonly Error VisitNotFound =
            new("Invoice.VisitNotFound", "Không tìm thấy thông tin lượt khám để tạo hóa đơn.");

        public static readonly Error NoItemsToBill =
            new("Invoice.NoItemsToBill", "Không có dịch vụ nào để tính tiền.");

        public static readonly Error AlreadyInvoiced =
            new("Invoice.AlreadyInvoiced", "Lượt khám này đã được xuất hóa đơn trước đó.");
    }
}