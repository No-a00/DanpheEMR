using DanpheEMR.Core.Domain.BloodBank;


namespace DanpheEMR.Core.Interface.BloodBank
{
    public interface IBloodGroupRepository 
     {
        Task<BloodGroup> GetByIdAsync(Guid Id);
        Task<BloodGroup> GetByNameAsync(string bloodGroupName);
        Task<BloodGroup> GetWithDonorsAsync(Guid Id);//lấy thông tin nhóm máu cùng với danh sách người hiến máu        

        Task<BloodGroup> AddAsync(BloodGroup bloodGroup); //(cập nhật lần đầu và có thể không cần thay đổi)
    }
}
