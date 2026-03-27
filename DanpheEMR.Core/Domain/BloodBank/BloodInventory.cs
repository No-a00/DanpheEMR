using System;
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

        // Mã vạch / Mã bịch máu vật lý (VD: BAG-2026-001)
        [Required, MaxLength(50)]
        public string BagNumber { get; set; }

        // Thể tích bịch máu (VD: 250ml, 350ml, 450ml)
        public int VolumeInMl { get; set; }

        public DateTime CollectionDate { get; set; }
        public DateTime ExpiryDate { get; set; }

        public BloodBagStatus Status { get; set; }


        public Guid BloodGroupId { get; set; }
        [ForeignKey("BloodGroupId")]
        public virtual BloodGroup BloodGroup { get; set; }

        // Có thể null nếu máu được nhập từ bệnh viện khác chuyển đến (không có data người hiến)
        public Guid? BloodDonorId { get; set; }
        [ForeignKey("BloodDonorId")]
        public virtual BloodDonor BloodDonor { get; set; }
    }
}