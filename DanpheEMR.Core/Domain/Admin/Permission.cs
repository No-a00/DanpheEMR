using DanpheEMR.Core.Domain.Base;
using System.ComponentModel.DataAnnotations;

namespace DanpheEMR.Core.Domain.Admin
{
    public class Permission : BaseEntity
    {
        public Guid Id { get; set; }
        [Required]
        public string PerCode { get; set; }
        public string Description { get; set; }
        public string Action { get; set; }
        public string Resource { get; set; }
        public ICollection<RolePermission> RolePermissions { get; set; }
    }
}

