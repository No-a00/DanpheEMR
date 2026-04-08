using DanpheEMR.Core.Domain.EMR;
using DanpheEMR.Core.Interface.Base;
namespace DanpheEMR.Core.Interfaces.EMR
{
    public class ClinicalNoteFilter
    {
        public Guid? PatientId { get; set; }
        public Guid? ProviderId { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public string ChiefComplaintKeyword { get; set; }
    }
    public interface IClinicalNoteRepository  : IGenericRepository<ClinicalNote>
    {
        Task<IEnumerable<ClinicalNote>> SearchNotesAsync(ClinicalNoteFilter filter);
        Task<ClinicalNote> GetNoteByVisitIdAsync(Guid visitId);

    }
}
