using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DanpheEMR.Core.Domain.Base;
using DanpheEMR.Core.Domain.Patients; 

namespace DanpheEMR.Core.Domain.BloodBank
{
    public class BloodIssue : BaseEntity
    {
        [Key]
        public Guid Id { get; set; }

        public DateTime IssueDate { get; set; }
        public string Remarks { get; set; }

        public Guid IssuedByUserId { get; set; }

        


        public Guid PatientId { get; set; }
        public virtual Patient Patient { get; set; }
        public Guid BloodInventoryId { get; set; }
        public virtual BloodInventory BloodInventory { get; set; }
    }
}