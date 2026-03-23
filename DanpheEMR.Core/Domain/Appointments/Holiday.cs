using DanpheEMR.Core.Domain.Admin;


namespace DanpheEMR.Core.Domain.Appointments
{
    public class Holiday
    {
        public Guid Id { get; set; }
        public DateTime Date { get; set; }
        public string Reason { get; set; }
        public bool IsGlobal { get; set; } // Nếu là ngày lễ toàn quốc, tất cả các phòng khám đều đóng cửa
        public int? ProviderId { get; set; }
        public Employee Provider { get; set; } // Nếu là ngày lễ riêng của một phòng khám cụ thể
    }
}
