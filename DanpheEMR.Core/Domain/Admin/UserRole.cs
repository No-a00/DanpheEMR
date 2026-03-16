using DanpheEMR.Core.Domain.Base;


namespace DanpheEMR.Core.Domain.Admin
{
    public class UserRole : BaseEntity
    {
        public int Id { get; set; }
        public bool IsActive { get; set; }
        public int UserId { get; set; }
        public int RoleId { get; set; }
        public User User { get; set; }
        public Role Role { get; set; }
    }
}
