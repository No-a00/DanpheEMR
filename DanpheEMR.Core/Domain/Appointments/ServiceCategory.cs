using DanpheEMR.Core.Domain.Base;
using DanpheEMR.Core.Domain.Billing;
using System.ComponentModel.DataAnnotations;

namespace DanpheEMR.Core.Domain.Appointments
{
    // Kế thừa BaseEntity để biết ai là người tạo/cập nhật nhóm này
    public class ServiceCategory : BaseEntity
    {
        [Key]
        public int Id { get; set; }

        [Required, MaxLength(50)]
        public string CategoryCode { get; set; } // Mã nhóm (VD: LAB - Xét nghiệm, RAD - X-Quang, CON - Khám bệnh)

        [Required, MaxLength(100)]
        public string CategoryName { get; set; } // Tên nhóm (VD: Xét nghiệm máu, Chẩn đoán hình ảnh)

        [MaxLength(255)]
        public string Description { get; set; } // Mô tả thêm (nếu cần)

        // Navigation Property: 1 Nhóm chứa rất nhiều Dịch vụ bên trong
        public virtual ICollection<ServiceItem> ServiceItems { get; set; }

        // Khởi tạo List để tránh lỗi NullReferenceException khi dùng .Add()
        public ServiceCategory()
        {
            ServiceItems = new HashSet<ServiceItem>();
        }
    }
}