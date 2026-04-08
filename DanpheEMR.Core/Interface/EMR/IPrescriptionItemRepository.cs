

using DanpheEMR.Core.Domain.EMR;
using DanpheEMR.Core.Interface.Base;

namespace DanpheEMR.Core.Interface.EMR
{
    public interface IPrescriptionItemRepository : IGenericRepository<PrescriptionItem>
    {
        //  Lấy toàn bộ các loại thuốc CỦA MỘT TỜ ĐƠN cụ thể
        Task<IEnumerable<PrescriptionItem>> GetItemsByPrescriptionIdAsync(Guid prescriptionId);

        // Thống kê truy vết: Xem một loại thuốc (ItemId) đã được kê trong những đơn nào
        // (Cực kỳ hữu ích khi có lệnh thu hồi thuốc khẩn cấp, bệnh viện cần tìm ngay những ai đã được kê loại thuốc này)
        Task<IEnumerable<PrescriptionItem>> GetItemsByDrugIdAsync(Guid itemId);
    }
}