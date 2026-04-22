using DanpheEMR.Core.Domain.Admin;
using DanpheEMR.Core.Domain.Base;
using DanpheEMR.Core.Domain.Patients;

namespace DanpheEMR.Core.Domain.EMR
{
    public class Prescription : BaseEntity,ISoftDelete
    {
        public Guid Id { get; set; }
        public DateTime PrescriptionDate { get; set; }
        public string Notes { get; set; }
        public string Status { get; set; }
        // Thông tin xóa mềm
        public bool IsDeleted { get; set; }

        public string? Reason { get; set; }
        public Guid? DeletedBy { get; set; }

        public Guid VisitId { get; set; }
        
        public Guid PatientId { get; set; }
        
        public Guid PrescriberId { get; set; }
        public Visit Visit { get; set; }
        public Patient Patient { get; set; }
        public Employee Prescriber { get; set; } // Liên kết đến nhân viên/bác sĩ kê đơn
        public ICollection<PrescriptionItem> Items { get; set; }
    }
}
