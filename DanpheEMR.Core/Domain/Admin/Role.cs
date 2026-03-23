using DanpheEMR.Core.Domain.Base;
using System.ComponentModel.DataAnnotations;

namespace DanpheEMR.Core.Domain.Admin
{
    // Kế thừa BaseEntity là quá chuẩn: Phải biết ai là người tạo ra Role này!
    public class Role : BaseEntity
    {
        [Key]
        public Guid Id { get; set; }

        [Required, MaxLength(50)]
        public string RoleName { get; set; } // Tên vai trò (VD: Admin, Doctor, Nurse)

        [MaxLength(255)]
        public string Description { get; set; } // Mô tả (VD: Bác sĩ có quyền khám bệnh)

        // Đã xóa dòng ICollection<User> để dùng chuẩn mô hình N-N tường minh qua bảng trung gian
        public virtual ICollection<UserRole> UserRoles { get; set; }

        // Quan hệ N-N với bảng Permission (Quyền hạn chi tiết)
        public virtual ICollection<RolePermission> RolePermissions { get; set; }

        // Khởi tạo danh sách để chống lỗi NullReferenceException
        public Role()
        {
            UserRoles = new HashSet<UserRole>();
            RolePermissions = new HashSet<RolePermission>();
        }
    }
}