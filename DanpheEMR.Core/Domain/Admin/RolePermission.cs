using DanpheEMR.Core.Domain.Base;

namespace DanpheEMR.Core.Domain.Admin
{
    public class RolePermission : BaseEntity
    {
        public Guid Id { get; set; }
        public bool IsActive { get; set; }
        public Guid RoleId { get; set; }
        public Guid PermissionId { get; set; }
        public  Role Role { get; set; }
        public Permission Permission { get; set; }
    }
}
