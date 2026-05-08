using DanpheEMR.Core.Domain.Base;
using DanpheEMR.Core.Domain.Patients;
using System.ComponentModel.DataAnnotations;


namespace DanpheEMR.Core.Domain.Pharmacy
{
    public class Dispense : BaseEntity
    {
        public Guid Id { get; set; }
        [Required]
        public string Code { get; set; }
        public Guid? PatientId { get; set; }
        public Guid? PrescriptionId { get; set; }

        public Guid StoreId { get; set; }

        public DateTime DispenseDate { get; set; }
        public string InvoiceNo { get; set; }
        public string Remarks { get; set; }

        public decimal TotalAmount { get; set; }
        public decimal DiscountAmount { get; set; }
        public decimal NetAmount { get; set; }

        public string DispenseStatus { get; set; } = "Dispensed";

        public virtual Patient Patient { get; set; }

       
        public virtual Store Store { get; set; }

        public virtual ICollection<DispenseItem> Items { get; set; }
    }
}