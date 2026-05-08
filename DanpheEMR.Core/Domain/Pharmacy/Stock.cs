
using DanpheEMR.Core.Domain.Base;
using System.ComponentModel.DataAnnotations;


namespace DanpheEMR.Core.Domain.Pharmacy
{
    public class Stock : BaseEntity
    {
        public Guid Id { get; set; }
        [Required]
        public string Code { get; set; }
        public string BatchNo { get; set; }  // Số lô hàng
        public DateTime ExpiryDate { get; set; }
        public int AvailableQuantity { get; set; }
        public decimal CostPrice { get; set; }
        public decimal SellingPrice { get; set; }
        public bool IsActive { get; set; } 
        public Guid ItemId { get; set; }
        public Guid StoreId { get; set; }
        public Item Item { get; set; }
        
        public Store Store { get; set; }
        
    }
}
