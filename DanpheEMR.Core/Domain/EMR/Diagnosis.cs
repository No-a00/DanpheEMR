using DanpheEMR.Core.Domain.Admin;
using DanpheEMR.Core.Domain.Base;
using DanpheEMR.Core.Domain.Patients;
using System;

namespace DanpheEMR.Core.Domain.EMR
{
    public class Diagnosis : BaseEntity,ISoftDelete
    {
        public Guid Id { get; set; }
        public DateTime DiagnosisDate { get; set; }
        public string ICD10Code { get; set; } // Mã ICD-10
        public string Description { get; set; } // Mô tả chẩn đoán
        public bool IsPrimary { get; set; }


        // Thông tin xóa mềm
        public bool IsDeleted { get; set; }

        public string? Reason { get; set; }
       public string? DeletedBy { get; set; }

        public Guid VisitId { get; set; } 
        public Visit Visit { get; set; }

        public Guid PatientId { get; set; } 
        public Patient Patient { get; set; }

        public Guid ProviderId { get; set; }
        public Employee Provider { get; set; }
    }
}