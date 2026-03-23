using DanpheEMR.Core.Domain.Base;
using DanpheEMR.Core.Domain.Appointments; // Thêm để dùng DoctorSchedule
using System.Collections.Generic;

namespace DanpheEMR.Core.Domain.Admin
{
    public class Employee : BaseEntity, ISoftDelete
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        // Thuộc tính tiện ích: Tự động ghép tên
        public string FullName => $"{FirstName} {LastName}";

        public bool IsDeleted { get; set; }
        public bool IsActive { get; set; } = true; // Mặc định là đang hoạt động

        public DateTime DOB { get; set; }
        public string Gender { get; set; }
        public string ContactNumber { get; set; }

        // Quan hệ với Department (Chuẩn 100%)
        public Guid DepartmentId { get; set; }
        public virtual Department Department { get; set; }

        // Quan hệ với User (Lớp bảo mật)
        public virtual ICollection<User> Users { get; set; }

        // --- THÊM DÒNG NÀY ĐỂ PHỤC VỤ ĐẶT LỊCH ---
        // Một bác sĩ có nhiều khung giờ trực/lịch làm việc
        public virtual ICollection<DoctorSchedule> Schedules { get; set; }
    }
}