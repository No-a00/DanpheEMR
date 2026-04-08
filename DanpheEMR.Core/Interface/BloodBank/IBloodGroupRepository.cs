using DanpheEMR.Core.Domain.BloodBank;
using DanpheEMR.Core.Enums;
using DanpheEMR.Core.Interface.Base;


namespace DanpheEMR.Core.Interface.BloodBank
{
    public interface IBloodGroupRepository  : IGenericRepository<BloodGroup>
    {
        Task<BloodGroup> GetByNameAsync(BloodType bloodGroupName);
        Task<BloodGroup> GetWithDonorsAsync(Guid Id);//lấy thông tin nhóm máu cùng với danh sách người hiến máu        
    }
}
