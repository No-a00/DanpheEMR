using DanpheEMR.Core.Domain.EMR;
using DanpheEMR.Core.Interface.Base;


namespace DanpheEMR.Core.Interface.EMR
{
    public interface IProgressNoteRepository : IGenericRepository<ProgressNote>
    {
        // Lấy toàn bộ Tờ điều trị (Diễn tiến) CỦA MỘT ĐỢT NẰM VIỆN
        Task<IEnumerable<ProgressNote>> GetNotesByAdmissionIdAsync(Guid admissionId);

        // Trưởng khoa muốn kiểm tra xem hôm nay Bác sĩ A đã đi buồng và viết bệnh án cho bao nhiêu bệnh nhân
        Task<IEnumerable<ProgressNote>> GetNotesByProviderAsync(Guid providerId, DateTime date);
    }
}