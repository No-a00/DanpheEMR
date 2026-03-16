using DanpheEMR.Core.Domain.Base;

namespace DanpheEMR.Core.Domain.Appointment
{
    public class PrescriptionItem : BaseEntity
    {
        public int Id { get; set; }
        public string Dosage { get; set; } // Liều lượng
        public string Frequency { get; set; } // Tần suất sử dụng
        public int DurationDays { get; set; } // Thời gian sử dụng (số ngày)
        public string Route { get; set; } // Ghi chú thêm
        public int PresscriptionId { get; set; } // Khóa ngoại đến Prescription
        public int ItemId { get; set; } // Khóa ngoại đến Item
        public Prescription Prescription { get; set; } // Navigation property đến Prescription
        public Item Item { get; set; } // Navigation property đến Item
    }
}
