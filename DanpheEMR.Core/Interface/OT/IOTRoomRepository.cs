using DanpheEMR.Core.Domain.OT;
using DanpheEMR.Core.Interface.Base; // Nhớ using IGenericRepository
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DanpheEMR.Core.Interface.OT
{
    public interface IOTRoomRepository : IGenericRepository<OTRoom>
    {
        // Lấy danh sách các phòng ĐANG HOẠT ĐỘNG (IsAvailable = true)
        Task<IEnumerable<OTRoom>> GetAvailableRoomsAsync();

        // Kiểm tra trùng tên phòng mổ
        Task<bool> IsRoomNameExistsAsync(string roomName, Guid? excludeId = null);
    }
}