using DanpheEMR.Core.Domain.Base;

namespace DanpheEMR.Core.Domain.Pharmacy
{
    public class Supplier : BaseEntity, IHasActiveStatus
    {
        public Guid Id { get; set; }
        public string SupplierCode { get; set; } 
        public string SupplierName { get; set; } 
        public string ContactPerson { get; set; } 
        public string ContactNumber { get; set; } 
        public string Email { get; set; }
        public string Address { get; set; }
        public bool IsActive { get; set; }
        public string CancelReason { get; set; }
        public int? CancelledByUserId { get; set; }
    }
}