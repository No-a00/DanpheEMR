using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DanpheEMR.Core.Domain.EMR
{
    public interface IDoctorOrderRepository
    {
        
        Task<DoctorOrder> GetByIdAsync(int id);
        Task<DoctorOrder> AddAsync(DoctorOrder order);
        Task UpdateAsync(DoctorOrder order);
        Task CancelOrderAsync(int orderId, string cancelReason, int cancelledByUserId);
        // Xem toàn bộ y lệnh của một lượt khám (Dành cho màn hình chi tiết bệnh án)
        Task<IEnumerable<DoctorOrder>> GetOrdersByVisitIdAsync(int visitId);

        // Lọc y lệnh do Bác sĩ A chỉ định trong khoảng thời gian nhất định (Để bác sĩ kiểm tra lại)
        Task<IEnumerable<DoctorOrder>> GetOrdersByProviderAsync(int providerId, DateTime fromDate, DateTime toDate);

        // Lọc y lệnh theo TRẠNG THÁI (Cực kỳ quan trọng cho Điều dưỡng)
        Task<IEnumerable<DoctorOrder>> GetOrdersByStatusAsync(string status);

        // Cập nhật nhanh trạng thái y lệnh (Từ Pending -> Completed khi y tá tiêm xong)
        Task UpdateOrderStatusAsync(int orderId, string newStatus);
    }
}