using DanpheEMR.Core.Domain.Base;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace DanpheEMR.Core.Domain.Patients
{
    public class PatientKin : BaseEntity
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        [NotMapped] 
        public string FullName => $"{LastName} {FirstName}".Trim();

        public string Relation { get; set; } 
        public string ContactNumber { get; set; }

        public Guid PatientId { get; set; }
        public virtual Patient Patient { get; set; }
    }
}