using DanpheEMR.Core.Domain.Base;

namespace DanpheEMR.Core.Domain.Admin
{
    public class Permission : BaseEntity
    {
        public Guid Id { get; set; }
        public string Description { get; set; }
        public string Action { get; set; }
        public string Resource { get; set; }
        public ICollection<RolePermission> RolePermissions { get; set; }
    }
}

