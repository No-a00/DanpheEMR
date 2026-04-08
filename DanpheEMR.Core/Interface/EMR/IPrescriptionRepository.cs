using DanpheEMR.Core.Domain.EMR;
using DanpheEMR.Core.Interface.Base;


namespace DanpheEMR.Core.Interface.EMR
{
    public interface IPrescriptionRepository : IGenericRepository<Prescription>
    {
        
        //  Lấy Đơn thuốc KÈM THEO danh sách các loại thuốc bên trong (Eager Loading)
        Task<Prescription> GetPrescriptionWithItemsAsync(Guid Id);

        // Mở hồ sơ bệnh án của ngày hôm nay lên xem có kê đơn gì không
        Task<IEnumerable<Prescription>> GetPrescriptionsByVisitIdAsync(Guid visitId);

        // Xem lại toàn bộ lịch sử uống thuốc của bệnh nhân từ trước đến nay
        Task<IEnumerable<Prescription>> GetPrescriptionsByPatientIdAsync(Guid patientId);

        // Bác sĩ muốn xem hôm nay mình đã kê bao nhiêu đơn
        Task<IEnumerable<Prescription>> GetPrescriptionsByPrescriberAsync(Guid prescriberId, DateTime date);

        // Dược sĩ lọc các đơn "Active/Pending" để gọi tên bệnh nhân ra nhận thuốc
        Task<IEnumerable<Prescription>> GetPrescriptionsByStatusAsync(string status);
    }
}