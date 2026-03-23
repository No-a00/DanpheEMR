
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

        public int WardId { get; set; }
        public Ward Ward { get; set; }

        public int BedFeatureId { get; set; }
        public BedFeature BedFeature { get; set; }

        public bool IsActive { get; set; } = true;
        public string CancelReason { get; set; }
        public int? CancelledByUserId { get; set; }
    }
}