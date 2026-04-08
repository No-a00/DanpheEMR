using DanpheEMR.Core.Domain.Base;


namespace DanpheEMR.Core.Domain.Admin
{
    public class UserRole : BaseEntity, ISoftDelete
    {
        public Guid Id { get; set; }

        // Thông tin xóa mềm
        public bool IsDeleted { get; set; }
        
        public string Reason { get; set; }
        public Guid? DeletedBy { get; set; }

        public Guid UserId { get; set; }
        public Guid RoleId { get; set; }

        public User User { get; set; }
        public Role Role { get; set; }
    }
}