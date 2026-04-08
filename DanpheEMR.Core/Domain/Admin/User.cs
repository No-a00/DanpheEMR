using DanpheEMR.Core.Domain.Base;
namespace DanpheEMR.Core.Domain.Admin
{
    public class User : BaseEntity,ISoftDelete
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public string PasswordHash { get; set; }
        public string Email { get; set; }
        public bool IsActive { get; set; } // Trạng thái hoạt động của người dùng
                                           // Nếu IsActive = false, người dùng sẽ không thể đăng nhập vào hệ thống

        // Thông tin xóa mềm
        public bool IsDeleted { get; set; }
        public string Reason { get; set; }
        public Guid? DeletedBy { get; set; }

        public Guid? EmployeeId { get; set; }
        public Employee Employee { get; set; }
        public ICollection<UserRole> UserRoles { get; set; }
        public ICollection<AuditLog> AuditLogs{ get; set; }
    }
}
