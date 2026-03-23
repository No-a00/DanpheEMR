
using DanpheEMR.Core.Domain.Base;
using DanpheEMR.Core.Domain.Patients;

namespace DanpheEMR.Core.Domain.Admin
{
    public class Department : BaseEntity
    {
        public Guid Id { get; set; }
        public string DepartmentCode { get; set; }
        public string DepartmentName { get; set; }
        public bool IsClinical { get; set; } // Phân biệt khoa lâm sàng và cận lâm sàng
        public bool IsActive { get; set; } // Trạng thái hoạt động của khoa
        public Guid ? ParentDepartmentId { get; set; } // Khoa cha (nếu có)
        public Department ParentDepartment { get; set; } // Navigation property đến khoa cha
        public Guid? HeadOfDepartmentId { get; set; } // ID của Trưởng khoa
        public Employee HeadOfDepartment { get; set; } // Navigation property
        public ICollection<Department> SubDepartments { get; set; } // Navigation property đến các khoa con
        public ICollection<Employee> Employees { get; set; } // Navigation property đến nhân viên thuộc khoa này
        public ICollection<Visit> Visits { get; set; } // Navigation property đến các lượt khám thuộc khoa này
    }
}
