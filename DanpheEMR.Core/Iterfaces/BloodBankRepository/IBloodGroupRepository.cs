using DanpheEMR.Core.Domain.Appointment;
using DanpheEMR.Core.Domain.BloodBank;
using DanpheEMR.Core.Iterface.Base;

namespace DanpheEMR.Core.Iterfaces.BloodBank
{
    public interface IBloodGroupRepository 
     {
        Task<BloodGroup> GetByIdAsync(int id);
        Task<BloodGroup> GetByNameAsync(string bloodGroupName);
        Task<BloodGroup> GetWithDonorsAsync(int id);//lấy thông tin nhóm máu cùng với danh sách người hiến máu        

        Task<BloodGroup> AddAsync(BloodGroup bloodGroup); //(cập nhật lần đầu và có thể không cần thay đổi)
    }
}
