using DanpheEMR.Core.Domain.OT;

namespace DanpheEMR.Core.Iterfaces.OT
{
    public interface IOTRoomRepository
    {
        Task<OTRoom> GetByIdAsync(int id);
        Task<IEnumerable<OTRoom>> GetAllAsync();

        Task<OTRoom> AddAsync(OTRoom room);
        Task UpdateAsync(OTRoom room);
        // Lấy danh sách các phòng ĐANG HOẠT ĐỘNG (IsAvailable = true)
        Task<IEnumerable<OTRoom>> GetAvailableRoomsAsync();
    }
}
