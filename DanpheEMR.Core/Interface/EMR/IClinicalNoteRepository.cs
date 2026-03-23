using DanpheEMR.Core.Domain.EMR;
namespace DanpheEMR.Core.Interfaces.EMR
{
    public class ClinicalNoteFilter
    {
        public int? PatientId { get; set; }
        public int? ProviderId { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public string ChiefComplaintKeyword { get; set; }
    }
    public interface IClinicalNoteRepository  
    {
        Task<ClinicalNote> GetByIdAsync(Guid Id);
        Task<ClinicalNote> AddAsync(ClinicalNote note);
        Task UpdateAsync(ClinicalNote note);

        // Xóa y khoa (Hủy và ghi lại lý do)
        Task VoidNoteAsync(int noteId, string voidReason, int voidedByUserId);

        // Gộp lọc để tránh gọi GetAll() làm sập server
        Task<IEnumerable<ClinicalNote>> SearchNotesAsync(ClinicalNoteFilter filter);
        Task<ClinicalNote> GetNoteByVisitIdAsync(int visitId);

    }
}
