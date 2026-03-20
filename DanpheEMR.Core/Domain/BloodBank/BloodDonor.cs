using DanpheEMR.Core.Domain.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DanpheEMR.Core.Domain.BloodBank
{
    public class BloodDonor : BaseEntity,ISoftDelete
    {
        [Key]
        public int Id { get; set; }

        [Required, MaxLength(100)]
        public string DonorName { get; set; }

        [Required, MaxLength(20)]
        public string Contact { get; set; }
        //soft Delete
        public bool IsDeleted { get; set; }

        // --- CÁC TRƯỜNG THÊM MỚI ĐỂ XÉT ĐIỀU KIỆN ---
        public DateTime DateOfBirth { get; set; }

        [MaxLength(10)]
        public string Gender { get; set; }

        public float Weight { get; set; }

        public bool IsPermanentlyDeferred { get; set; } // Cờ đánh dấu bị cấm hiến vĩnh viễn 

        public int TotalDonations { get; set; } // Tổng số lần đã hiến (Có thể tăng tự động mỗi lần hiến thành công)

        public DateTime? LastDonatedDate { get; set; }


        public int BloodGroupId { get; set; }
        [ForeignKey("BloodGroupId")]
        public virtual BloodGroup BloodGroup { get; set; }


        
        public bool IsEligibleToDonate
        {
            get
            {

                if (IsPermanentlyDeferred) return false;

                var age = DateTime.Now.Year - DateOfBirth.Year;
                if (DateOfBirth.Date > DateTime.Now.AddYears(-age)) age--; // Trừ hao nếu chưa qua sinh nhật
                if (age < 18 || age > 60) return false;


                if (Weight < 45) return false;

                // 4. Kiểm tra thời gian giãn cách (Giữ nguyên logic cực hay của bạn)
                if (!LastDonatedDate.HasValue) return true;


                var daysSinceLastDonation = (DateTime.Now - LastDonatedDate.Value).TotalDays;
                return daysSinceLastDonation >= 84;
            }
        }
    }
}