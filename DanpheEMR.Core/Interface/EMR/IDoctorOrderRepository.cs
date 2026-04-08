using DanpheEMR.Core.Domain.EMR;
using DanpheEMR.Core.Interface.Base;


namespace DanpheEMR.Core.Interface.EMR
{
    public interface IDoctorOrderRepository : IGenericRepository<DoctorOrder>   
    {
        Task<IEnumerable<DoctorOrder>> GetOrdersByVisitIdAsync(Guid visitId);

        Task<IEnumerable<DoctorOrder>> GetPendingOrdersAsync();

        // Lọc y lệnh do Bác sĩ A chỉ định trong khoảng thời gian nhất định (Để bác sĩ kiểm tra lại)
        Task<IEnumerable<DoctorOrder>> GetOrdersByProviderAsync(Guid providerId, DateTime fromDate, DateTime toDate);

        // Lọc y lệnh theo TRẠNG THÁI (Cực kỳ quan trọng cho Điều dưỡng)
        Task<IEnumerable<DoctorOrder>> GetOrdersByStatusAsync(string status);

        // Cập nhật nhanh trạng thái y lệnh (Từ Pending -> Completed khi y tá tiêm xong)
        Task UpdateOrderStatusAsync(Guid orderId, string newStatus);
    }
}