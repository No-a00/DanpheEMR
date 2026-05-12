using System.ComponentModel.DataAnnotations;
using DanpheEMR.Core.Domain.Base;
using DanpheEMR.Core.Domain.Admin; 
namespace DanpheEMR.Core.Domain.Billing
{
    public class BillingTransactionItem : BaseEntity
    {
        [Key]
        public Guid Id { get; set; }
     

        [Required]
        public string ItemName { get; set; }
        public decimal Quantity { get; set; }
        public decimal Price { get; set; } 
        public decimal SubTotal { get; set; } 
        public decimal DiscountAmount { get; set; } 

        public decimal TaxAmount { get; set; } 
        public decimal TotalAmount { get; set; } 
        
        public Guid? ProviderId { get; set; }
        public virtual Employee Provider { get; set; }
        public Guid BillingTransactionId { get; set; }
        public virtual BillingTransaction BillingTransaction { get; set; }
        public Guid ServiceItemId { get; set; }
        public virtual ServiceItem ServiceItem { get; set; }
    }
}