using DanpheEMR.Core.Domain.Base;
using System;

namespace DanpheEMR.Core.Domain.Admin
{
    public class UserRole : BaseEntity
    {
        public Guid Id { get; set; }
        public bool IsActive { get; set; }
        public Guid UserId { get; set; }
        public Guid RoleId { get; set; }

        public User User { get; set; }
        public Role Role { get; set; }
    }
}