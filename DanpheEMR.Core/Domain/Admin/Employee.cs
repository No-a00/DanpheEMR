using DanpheEMR.Core.Domain.Base;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace DanpheEMR.Core.Domain.Admin
{
    public class Employee : BaseEntity, ISoftDelete
    {
        public Guid Id { get; set; }
        [Required]
        public string Code { get; set; }
        public string Workforce { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName => $"{FirstName} {LastName}";

        public DateTime DOB { get; set; }
        public string Gender { get; set; }

        public bool IsActive { get; set; }//kiểm tra xem nhân viên có đang làm việc hay không, nếu nghỉ việc thì sẽ không được phép tạo lịch khám

        public bool IsDeleted { get; set; }
        public  string? Reason { get; set; }
        public Guid? DeletedBy { get; set; }

        public string ContactNumber { get; set; }
        public Guid DepartmentId { get; set; }
        public virtual Department Department { get; set; }
        public virtual ICollection<User> Users { get; set; }
        public virtual ICollection<DoctorSchedule> Schedules { get; set; }
    }
}