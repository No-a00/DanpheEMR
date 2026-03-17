
using DanpheEMR.Core.Domain.Appointments;
using DanpheEMR.Core.Domain.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DanpheEMR.Core.Domain.Billing
{
    // 1. Nhớ kế thừa BaseEntity
    public class ServiceItem : BaseEntity
    {
        [Key]
        public int Id { get; set; }

        [Required, MaxLength(50)]
        public string ItemCode { get; set; } // Mã dịch vụ (VD: XQ01, XN02)

        [Required, MaxLength(200)]
        public string ItemName { get; set; } // Tên dịch vụ (VD: Chụp X-Quang Phổi)

        [Column(TypeName = "decimal(18,2)")] // Định dạng tiền tệ chuẩn trong SQL
        public decimal Price { get; set; }

        public bool IsTaxable { get; set; } // Có tính thuế VAT hay không?

        // 2. Bổ sung Navigation Property chuẩn EF Core
        public int ServiceCategoryId { get; set; }

        [ForeignKey("ServiceCategoryId")]
        public ServiceCategory Category { get; set; } // Liên kết sang bảng Nhóm Dịch Vụ
    }
}