
using DanpheEMR.Core.Domain.Base;

namespace DanpheEMR.Core.Domain.Admin
{
    public class Employee : BaseEntity, ISoftDelete
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        //soft delete
        public bool IsDeleted { get; set; }

        public DateTime DOB { get; set; }
        public string Gender { get; set; }
        public string ContactNumber { get; set; }
        public Guid DepartmentId { get; set; }
        public Department Department { get; set; }
        public ICollection<User> Users { get; set; }
    }
}
