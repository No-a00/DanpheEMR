using DanpheEMR.Core.Domain.Base;
using System.ComponentModel.DataAnnotations;

namespace DanpheEMR.Core.Domain.Admin
{
    public class Role : BaseEntity,ISoftDelete
    {
        [Key]
        public Guid Id { get; set; }
        public string RoleName { get; set; }
        public string Description { get; set; } 
        public virtual ICollection<UserRole> UserRoles { get; set; }
        public bool IsDelete { get; set; }
        public string Reason { get; set; }
        public Guid? DeletedBy { get; set; }
        public virtual ICollection<RolePermission> RolePermissions { get; set; }
        public Role()
        {
            UserRoles = new HashSet<UserRole>();
            RolePermissions = new HashSet<RolePermission>();
        }
    }
}