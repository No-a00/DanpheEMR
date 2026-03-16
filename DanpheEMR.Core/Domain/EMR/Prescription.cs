using DanpheEMR.Core.Domain.Admin;
using DanpheEMR.Core.Domain.ADTModels;
using DanpheEMR.Core.Domain.Base;
using DanpheEMR.Core.Domain.Nums;
using System.ComponentModel.DataAnnotations.Schema;

namespace DanpheEMR.Core.Domain.Appointment
{
    public class Prescription : BaseEntity
    {
        public int Id { get; set; }
        public DateTime PrescriptionDate { get; set; }
        public string Notes { get; set; }
        public VisitStatus Status { get; set; }
        public int VisitId { get; set; }
        public Visit Visit { get; set; }
        public int PatientId { get; set; }
        public Patient Patient { get; set; }
        public int PrescriberId { get; set; }
        [ForeignKey("PrescriberId")]
        public Employee Prescriber { get; set; } // Liên kết đến nhân viên/bác sĩ kê đơn
        public ICollection<PrescriptionItem> Items { get; set; }
    }
}
