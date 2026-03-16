using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DanpheEMR.Core.Domain.EMR
{
    public interface IPrescriptionRepository
    {
        
        Task<Prescription> GetByIdAsync(int id);
        // Cực kỳ quan trọng: Lấy Đơn thuốc KÈM THEO danh sách các loại thuốc bên trong (Eager Loading)
        Task<Prescription> GetPrescriptionWithItemsAsync(int id);
        Task<Prescription> AddAsync(Prescription prescription);
        Task UpdateAsync(Prescription prescription);
        // Bác sĩ lỡ tay kê nhầm, hoặc bệnh nhân dị ứng thuốc -> Hủy đơn
        Task CancelPrescriptionAsync(int prescriptionId, string cancelReason);

        // Mở hồ sơ bệnh án của ngày hôm nay lên xem có kê đơn gì không
        Task<IEnumerable<Prescription>> GetPrescriptionsByVisitIdAsync(int visitId);

        // Xem lại toàn bộ lịch sử uống thuốc của bệnh nhân từ trước đến nay
        Task<IEnumerable<Prescription>> GetPrescriptionsByPatientIdAsync(int patientId);

        // Bác sĩ muốn xem hôm nay mình đã kê bao nhiêu đơn
        Task<IEnumerable<Prescription>> GetPrescriptionsByPrescriberAsync(int prescriberId, DateTime date);

        // Dược sĩ lọc các đơn "Active/Pending" để gọi tên bệnh nhân ra nhận thuốc
        Task<IEnumerable<Prescription>> GetPrescriptionsByStatusAsync(string status);
    }
}