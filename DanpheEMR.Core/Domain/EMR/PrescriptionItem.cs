using DanpheEMR.Core.Domain.Base;
using DanpheEMR.Core.Domain.Pharmacy;

namespace DanpheEMR.Core.Domain.EMR
{
    public class PrescriptionItem : BaseEntity
    {
        public Guid Id { get; set; }
        public string Dosage { get; set; } // Liều lượng
        public string Frequency { get; set; } // Tần suất sử dụng
        public int DurationInDays { get; set; } // Thời gian sử dụng (số ngày)
        public string Notes { get; set; } // Ghi chú thêm
        //hủy và lí do
        public bool IsActive { get; set; }
        public string CancelReason { get; set; }
        public int UserIdCancel { get; set; }
        //
        public Guid PrescriptionId { get; set; } // Khóa ngoại đến Prescription
        public Guid MedicineId { get; set; } // Khóa ngoại đến Medicine
        public Prescription Prescription { get; set; } // Navigation property đến Prescription
        public Item Item { get; set; } // Navigation property đến Item
    }
}
