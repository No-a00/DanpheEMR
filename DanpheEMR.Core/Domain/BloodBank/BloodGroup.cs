using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using DanpheEMR.Core.Domain.Base;
using DanpheEMR.Core.Enums;

namespace DanpheEMR.Core.Domain.BloodBank
{
    public class BloodGroup : BaseEntity
    {
        [Key]
        public Guid Id { get; set; }

        // Đã bỏ MaxLength vì BloodType là Enum
        [Required]
        public BloodType BloodGroupName { get; set; }

        [MaxLength(255)]
        public string Description { get; set; }

        public virtual ICollection<BloodDonor> BloodDonors { get; set; }

        public BloodGroup()
        {
            BloodDonors = new HashSet<BloodDonor>();
        }
    }
}