using DanpheEMR.Core.Domain.Admin;

namespace DanpheEMR.Core.Domain.Appointment
{
    public class Ward
    {
        public int Id { get; set; }
        public string WardCode { get; set; } // Mã khoa/phòng
        public string WardName { get; set; } // Tên khoa/phòng
        public int Floor { get; set; } // Tầng
        public int DepartmentId { get; set; } // Khóa ngoại đến Department
        public Department Department { get; set; } // Navigation property đến Department
    }
}
