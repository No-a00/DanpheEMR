using DanpheEMR.Core.Enums;
using DanpheEMR.Core.Domain.Admin;
using DanpheEMR.Core.Domain.Base;
using DanpheEMR.Core.Domain.EMR;
using System;
using System.Collections.Generic;

namespace DanpheEMR.Core.Domain.Patients
{
    public class Visit : BaseEntity, ISoftDelete
    {
        public Guid Id { get; set; }
        public string VisitCode { get; set; } 
        public int? QueueNo { get; set; } 
        public string ChiefComplaint { get; set; }
        public string VisitType { get; set; }
        public DateTime VisitDate { get; set; }
        public VisitStatus Status { get; set; }

        // Thông tin xóa mềm
        public bool IsDeleted { get; set; }

        public string? Reason { get; set; }
        public Guid? DeletedBy { get; set; } 

        public Guid PatientId { get; set; }
        public Guid DepartmentId { get; set; }
        public Guid ProviderId { get; set; }

        public virtual Patient Patient { get; set; }
        public virtual Department Department { get; set; }
        public virtual Employee Provider { get; set; }

        public virtual ICollection<ClinicalNote> ClinicalNotes { get; set; }
        public virtual ICollection<Diagnosis> Diagnoses { get; set; }
        public virtual ICollection<Prescription> Prescriptions { get; set; }
        public virtual ICollection<Vitals> Vitals { get; set; }
        public virtual ICollection<DoctorOrder> DoctorOrders { get; set; }
        public virtual ICollection<Admission> Admissions { get; set; }
    }
}