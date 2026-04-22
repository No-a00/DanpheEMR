using DanpheEMR.Core.Domain.Base;

namespace DanpheEMR.Core.Domain.Wards
{
    public class Ward : BaseEntity,ISoftDelete 
    {      
        public Guid Id { get; set; }
        public string WardCode { get; set; } // Mã buồng/khoa (VD: "ICU-01", "NOI-A")
        public string WardName { get; set; } 
        public string WardLocation { get; set; } 
        public int TotalBeds { get; set; }

        // Thông tin xóa mềm
        public bool IsDeleted { get; set; }

        public string? Reason { get; set; }

        public Guid? DeletedBy { get; set; }
        public ICollection<Bed> Beds { get; set; } = new List<Bed>();
    }
}