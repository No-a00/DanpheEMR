using DanpheEMR.Core.Domain.EMR;


namespace DanpheEMR.Core.Interface.EMR
{
    public interface IProgressNoteRepository
    {
        Task<ProgressNote> GetByIdAsync(Guid Id);
        Task<ProgressNote> AddAsync(ProgressNote note);
        Task UpdateAsync(ProgressNote note);

        // Cấm xóa vật lý hồ sơ y khoa. Chỉ cho phép đánh dấu Hủy nếu gõ sai hoàn toàn.
        Task VoidNoteAsync(Guid Id, string voidReason, Guid voidedByUserId);
        // Lấy toàn bộ Tờ điều trị (Diễn tiến) CỦA MỘT ĐỢT NẰM VIỆN
        // (Bắt buộc phải sắp xếp theo NoteDate giảm dần để bác sĩ thấy ngày mới nhất ở trên cùng)
        Task<IEnumerable<ProgressNote>> GetNotesByAdmissionIdAsync(Guid admissionId);

        // Trưởng khoa muốn kiểm tra xem hôm nay Bác sĩ A đã đi buồng và viết bệnh án cho bao nhiêu bệnh nhân
        Task<IEnumerable<ProgressNote>> GetNotesByProviderAsync(Guid providerId, DateTime date);
    }
}