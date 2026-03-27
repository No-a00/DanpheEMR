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

        [MaxLength(500)]
        public string Remarks { get; set; }

        public Guid IssuedByUserId { get; set; }

        

        // Xuất cho bệnh nhân nào?
        public Guid PatientId { get; set; }
        [ForeignKey("PatientId")]
        public virtual Patient Patient { get; set; }

        // Xuất bịch máu nào? (Quan hệ 1-1 với BloodInventory)
        public Guid BloodInventoryId { get; set; }
        [ForeignKey("BloodInventoryId")]
        public virtual BloodInventory BloodInventory { get; set; }
    }
}