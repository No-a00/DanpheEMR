using DanpheEMR.Core.Domain.Base;
using DanpheEMR.Core.Domain.Billing;
using System.ComponentModel.DataAnnotations;

namespace DanpheEMR.Core.Domain.Appointments
{
    public class ServiceCategory : BaseEntity
    {
        [Key]
        public Guid Id { get; set; }
        public string CategoryCode { get; set; } 
        public string CategoryName { get; set; } 
        public string Description { get; set; }
        public virtual ICollection<ServiceItem> ServiceItems { get; set; }
        public ServiceCategory()
        {
            ServiceItems = new HashSet<ServiceItem>();
        }
    }
}