using DanpheEMR.Core.Domain.Admin;
using DanpheEMR.Core.Domain.Base;
using DanpheEMR.Core.Domain.Patients;
using System;

namespace DanpheEMR.Core.Domain.EMR
{
    public class DoctorOrder : BaseEntity
    {
        public Guid Id { get; set; }
        public DateTime OrderDate { get; set; }
        public string OrderText { get; set; } = string.Empty;
        public string Status { get; set; }
        public Guid VisitId { get; set; }

        // Đã sửa Viết hoa chữ cái đầu và thêm dấu ? cho Guid
        public Guid? CancelledByUserId { get; set; }
        public bool IsActive { get; set; }
        public string CancelReason { get; set; }

        public Guid ProviderId { get; set; }

        public Visit Visit { get; set; }
        public Employee Provider { get; set; }
    }
}