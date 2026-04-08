using DanpheEMR.Core.Domain.Base;

namespace DanpheEMR.Core.Domain.Pharmacy
{
    public class Supplier : BaseEntity, ISoftDelete
    {
        public Guid Id { get; set; }
        public string SupplierCode { get; set; } 
        public string SupplierName { get; set; } 
        public string ContactPerson { get; set; } 
        public string ContactNumber { get; set; } 
        public string Email { get; set; }
        public string Address { get; set; }
        public bool IsActive { get; set; }
        // Thông tin xóa mềm
        public bool IsDeleted { get; set; }

        public string Reason { get; set; }

        public Guid? DeletedBy { get; set; }
    }
}