using DanpheEMR.Core.Domain.Base;
namespace DanpheEMR.Core.Domain.Wards
{
    public class Ward : BaseEntity
    {
        public int Id { get; set; }
        public string WardCode { get; set; } // Mã buồng/khoa (VD: "ICU-01", "NOI-A")
        public string WardName { get; set; } // Tên buồng/khoa (VD: "Khoa Hồi sức tích cực", "Buồng Nội A")
        public string WardLocation { get; set; } // Vị trí (VD: "Tầng 3, Tòa nhà B")
        public int TotalBeds { get; set; } // Tổng công suất giường của khoa này
        public bool IsActive { get; set; } = true;
        public ICollection<Bed> Beds { get; set; }
    }
}