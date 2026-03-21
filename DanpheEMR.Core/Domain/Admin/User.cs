using DanpheEMR.Core.Domain.Base;
namespace DanpheEMR.Core.Domain.Admin
{
    public class User : BaseEntity,IHasActiveStatus
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string PasswordHash { get; set; }
        public string Email { get; set; }
        public bool IsActive { get; set; } 
        public int? EmployeeId { get; set; }
        public Employee Employee { get; set; }
        public ICollection<UserRole> UserRoles { get; set; }
        public ICollection<AuditLog> AuditLogs{ get; set; }
    }
}
