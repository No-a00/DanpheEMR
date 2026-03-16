using DanpheEMR.Core.Domain.Base;
using System.ComponentModel.DataAnnotations;

namespace DanpheEMR.Core.Domain.BloodBank
{
    public class BloodGroup : BaseEntity
    {
        [Key]
        public int Id { get; set; }

        [Required, MaxLength(10)]
        public string BloodGroupName { get; set; } // Tên nhóm máu (VD: A+, O-, AB+)

        [MaxLength(255)]
        public string Description { get; set; } // Ghi chú thêm (VD: Nhóm máu hiếm)

        // Navigation Property: 1 Nhóm máu có rất nhiều Người hiến máu
        public virtual ICollection<BloodDonor> BloodDonors { get; set; }

        // Khởi tạo danh sách để tránh lỗi NullReferenceException
        public BloodGroup()
        {
            BloodDonors = new HashSet<BloodDonor>();
        }
    }
}