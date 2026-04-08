
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DanpheEMR.Core.Domain.Base;
using DanpheEMR.Core.Enums;

namespace DanpheEMR.Core.Domain.BloodBank
{
    public class BloodInventory : BaseEntity
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public string BagNumber { get; set; }
        public int VolumeInMl { get; set; }
        public DateTime CollectionDate { get; set; }
        public DateTime ExpiryDate { get; set; }

        public BloodBagStatus Status { get; set; }

        public Guid BloodGroupId { get; set; }
        public virtual BloodGroup BloodGroup { get; set; }
        // Có thể null nếu máu được nhập từ bệnh viện khác chuyển đến 
        public Guid? BloodDonorId { get; set; }
        [ForeignKey("BloodDonorId")]
        public virtual BloodDonor BloodDonor { get; set; }
    }
}