using DanpheEMR.Core.Domain.Base;

namespace DanpheEMR.Core.Domain.Admin
{
    public class RolePermission : BaseEntity
    {
        public int Id { get; set; }
        public bool IsActive { get; set; }
        public int RoleId { get; set; }
        public int PermissionId { get; set; }
        public  Role Role { get; set; }
        public Permission Permission { get; set; }
    }
}
