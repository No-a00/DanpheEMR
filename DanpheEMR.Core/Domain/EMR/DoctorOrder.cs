using DanpheEMR.Core.Domain.Admin;
using DanpheEMR.Core.Domain.Base;
using DanpheEMR.Core.Domain.Patients;
using System;

namespace DanpheEMR.Core.Domain.EMR
{
    public class DoctorOrder : BaseEntity,ISoftDelete
    {
        public Guid Id { get; set; }
        public DateTime OrderDate { get; set; }
        public string OrderText { get; set; } = string.Empty;
        public string Status { get; set; }
        public Guid VisitId { get; set; }

        // Thông tin xóa mềm
        public bool IsDeleted { get; set; }

        public string? Reason { get; set; }
       public string? DeletedBy { get; set; }

        public Guid ProviderId { get; set; }

        public Visit Visit { get; set; }
        public Employee Provider { get; set; }
    }
}