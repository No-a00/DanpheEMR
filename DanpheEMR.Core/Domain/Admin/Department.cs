
using DanpheEMR.Core.Domain.Base;
using DanpheEMR.Core.Domain.Patients;

namespace DanpheEMR.Core.Domain.Admin
{
    public class Department : BaseEntity
    {
        public int Id { get; set; }
        public string DepartmentCode { get; set; }
        public string DepartmentName { get; set; }
        public bool IsClinical { get; set; } // Phân biệt khoa lâm sàng và cận lâm sàng
        public int ? ParentDepartmentId { get; set; } // Khoa cha (nếu có)
        public Department ParentDepartment { get; set; } // Navigation property đến khoa cha
        public ICollection<Department> SubDepartments { get; set; } // Navigation property đến các khoa con
        public ICollection<Employee> Employees { get; set; } // Navigation property đến nhân viên thuộc khoa này
        public ICollection<Visit> Visits { get; set; } // Navigation property đến các lượt khám thuộc khoa này
    }
}
