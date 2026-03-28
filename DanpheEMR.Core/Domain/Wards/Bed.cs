
using DanpheEMR.Core.Domain.Base;
using DanpheEMR.Core.Enums;
namespace DanpheEMR.Core.Domain.Wards
{
    public class Bed : BaseEntity, IHasActiveStatus
    {
        public Guid Id { get; set; }

        public string BedCode { get; set; } // ĐÃ THÊM: Mã giường (VD: "B01")
        public string BedNumber { get; set; } 

        public bool IsOccupied { get; set; } = false; 

        public BedStatus Status { get; set; }

        public Guid WardId { get; set; }
        public Ward Ward { get; set; }

        public Guid BedFeatureId { get; set; }
        public BedFeature BedFeature { get; set; }

        public bool IsActive { get; set; }
        public string CancelReason { get; set; }
        public Guid? CancelledByUserId { get; set; }
    }
}