using DanpheEMR.Core.Domain.Admin;
using DanpheEMR.Core.Domain.Base;
using DanpheEMR.Core.Domain.Patients;
using System;

namespace DanpheEMR.Core.Domain.EMR
{
    public class Diagnosis : BaseEntity
    {
        public Guid Id { get; set; }
        public DateTime DiagnosisDate { get; set; }
        public string ICD10Code { get; set; } // Mã ICD-10
        public string Description { get; set; } // Mô tả chẩn đoán
        public bool IsPrimary { get; set; } // Chẩn đoán chính hay phụ

     
        public string Reason { get; set; }
        public bool IsDeleted { get; set; }
        public Guid? VoidedBy { get; set; } 

        public Guid VisitId { get; set; } 
        public Visit Visit { get; set; }

        public Guid PatientId { get; set; } 
        public Patient Patient { get; set; }

        public Guid ProviderId { get; set; }
        public Employee Provider { get; set; }
    }
}