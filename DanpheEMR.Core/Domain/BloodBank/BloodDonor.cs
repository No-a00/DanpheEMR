using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DanpheEMR.Core.Domain.Base;

namespace DanpheEMR.Core.Domain.BloodBank
{
    public class BloodDonor : BaseEntity, ISoftDelete
    {
        [Key]
        public Guid Id { get; set; }

        [Required, MaxLength(100)]
        public string DonorName { get; set; }

        [Required, MaxLength(20)]
        public string Contact { get; set; }

        // Thông tin xóa mềm
        public bool IsDeleted { get; set; }

        public string? Reason { get; set; }
       public string? DeletedBy { get; set; }

        public DateTime DateOfBirth { get; set; }
        public string Gender { get; set; }

        public float Weight { get; set; }

        public bool IsPermanentlyDeferred { get; set; }

        public int TotalDonations { get; set; }

        public DateTime? LastDonatedDate { get; set; }
        public Guid BloodGroupId { get; set; }

        [ForeignKey("BloodGroupId")]
        public virtual BloodGroup BloodGroup { get; set; }

        public bool IsEligibleToDonate
        {
            get
            {
                if (IsPermanentlyDeferred) return false;

                var age = DateTime.Today.Year - DateOfBirth.Year;
                if (DateOfBirth.Date > DateTime.Today.AddYears(-age)) age--;

                if (age < 18 || age > 60) return false;

                if (Weight < 45) return false;

                if (!LastDonatedDate.HasValue) return true;

                var daysSinceLastDonation = (DateTime.Today - LastDonatedDate.Value.Date).TotalDays;
                return daysSinceLastDonation >= 84;
            }
        }
    }
}