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

        [Required]
        public BloodType BloodGroupName { get; set; }

        public string Description { get; set; }

        public virtual ICollection<BloodDonor> BloodDonors { get; set; }

        public BloodGroup()
        {
            BloodDonors = new HashSet<BloodDonor>();
        }
    }
}