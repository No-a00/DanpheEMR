using DanpheEMR.Core.Domain.Base;

namespace DanpheEMR.Core.Domain.Wards
{
    public class Ward : BaseEntity, IHasActiveStatus
    {
        public Guid Id { get; set; }
        public string WardCode { get; set; } // Mã buồng/khoa (VD: "ICU-01", "NOI-A")
        public string WardName { get; set; } 
        public string WardLocation { get; set; } 
        public int TotalBeds { get; set; } 

        public bool IsActive { get; set; } = true;
        public string CancelReason { get; set; }
        public int? CancelledByUserId { get; set; }
        public ICollection<Bed> Beds { get; set; } = new List<Bed>();
    }
}