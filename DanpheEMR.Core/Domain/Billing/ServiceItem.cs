
using DanpheEMR.Core.Domain.Appointments;
using DanpheEMR.Core.Domain.Base;
using System.ComponentModel.DataAnnotations;

namespace DanpheEMR.Core.Domain.Billing
{
    public class ServiceItem : BaseEntity
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public string ItemCode { get; set; }

        [Required]
        public string ItemName { get; set; } 
        public decimal Price { get; set; }
        public bool IsTaxable { get; set; } // Thuế suất có thể áp dụng cho dịch vụ này

        public Guid ServiceCategoryId { get; set; }
        public ServiceCategory ServiceCategory { get; set; } 
    }
}