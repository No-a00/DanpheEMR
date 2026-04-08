using Application.Common;

namespace DanpheEMR.Application.Features.Pharmacy.Commands.AddSupplier
{
    public static class AddSupplierErrors
    {
        public static readonly Error DBError = new Error("Supplier.DBError", "Lỗi lưu thông tin nhà cung cấp.");
    }
}