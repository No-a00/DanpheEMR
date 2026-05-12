using DanpheEMR.Core.Domain.Base;
using System.ComponentModel.DataAnnotations;
namespace DanpheEMR.Core.Domain.Admin
{
    public class User : BaseEntity,ISoftDelete
    {
        public Guid Id { get; set; }
        [Required]
        public string Code { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string FullName { get; set; }
        public string PhoneNumber { get; set; }
        public string? AvatarUrl { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Gender { get; set; }


        public string?   PasswordHash { get; set; }
        //token để refresh access token khi access token hết hạn, mỗi lần đăng nhập sẽ tạo mới refresh token và lưu vào database
        public string?   RefreshToken { get; set; }
        public DateTime? RefreshTokenExpiryTime { get; set; } = DateTime.MinValue;



       
        public bool IsActive { get; set; } // Trạng thái hoạt động của người dùng
                                           // Nếu IsActive = false, người dùng sẽ không thể đăng nhập vào hệ thống

        // Thông tin xóa mềm
        public bool IsDeleted { get; set; }
        public string? Reason { get; set; }
       public string? DeletedBy { get; set; }

        public Guid? EmployeeId { get; set; }
        public Employee Employee { get; set; }
        public ICollection<UserRole> UserRoles { get; set; }
        public ICollection<AuditLog> AuditLogs{ get; set; }
    }
}
