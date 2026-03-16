namespace DanpheEMR.Core.Domain.Appointment
{
    public class Bed
    {
        public int Id { get; set; }
        public string BedNumber { get; set; }
        public bool IsOccupied { get; set; } = false; // Trạng thái giường: có bệnh nhân nằm hay không
        public string Status { get; set; } // Trạng thái giường: "Available", "Occupied", "Cleaning", "Maintenance"
        public int WardId { get; set; } // Khóa ngoại: Giường thuộc khoa nào
        public int BedFeatureId { get; set; } // Khóa ngoại: Tính năng giường (giường thường, giường ICU, giường cách ly...)

    }
}
