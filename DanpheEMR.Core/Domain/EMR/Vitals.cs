
using DanpheEMR.Core.Domain.Base;
using DanpheEMR.Core.Domain.Patients;

namespace DanpheEMR.Core.Domain.EMR
{
    public class Vitals : BaseEntity, ISoftDelete
    {
        public Guid Id { get; set; }
        public DateTime RecordedAt { get; set; }  // Thời điểm ghi nhận
        public int HeartRate { get; set; } // Nhịp tim (bpm)
        public string BloodPressure { get; set; } // Huyết áp (ví dụ: "120/80 mmHg")
        public decimal Temperature { get; set; } // Nhiệt độ cơ thể (°C)
        public int RespiratoryRate { get; set; } // Nhịp thở (lần/phút)
        public decimal SpO2 { get; set; } // Độ bão hòa oxy trong máu (%)
        public decimal Weight { get; set; } // Cân nặng (kg)
        public decimal Height { get; set; } // Chiều cao (cm)
        public decimal BMI { get; set; } // Chỉ số khối cơ thể (BMI)
        // Thông tin xóa mềm
        public bool IsDeleted { get; set; }

        public string Reason { get; set; }
        public Guid? DeletedBy { get; set; }

        public Guid VisitId { get; set; } // Khóa ngoại liên kết với Visit
        public Guid PatientId { get; set; } // Khóa ngoại liên kết với Patient
        public Visit Visit { get; set; } // Navigation property đến Visit
        public Patient Patient { get; set; } // Navigation property đến Patient 

    }
}
