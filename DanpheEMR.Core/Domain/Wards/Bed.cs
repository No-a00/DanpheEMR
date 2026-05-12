
using DanpheEMR.Core.Domain.Base;
using DanpheEMR.Core.Enums;
namespace DanpheEMR.Core.Domain.Wards
{
    public class Bed : BaseEntity, ISoftDelete
    {
        public Guid Id { get; set; }

        public string BedCode { get; set; }
        public string BedNumber { get; set; } 

        public bool IsOccupied { get; set; } = false; 

        public BedStatus Status { get; set; }

        public Guid WardId { get; set; }
        public Ward Ward { get; set; }

        public Guid BedFeatureId { get; set; }
        public BedFeature BedFeature { get; set; }

        // Thông tin xóa mềm
        public bool IsDeleted { get; set; }

        public string? Reason { get; set; }

       public string? DeletedBy { get; set; }
    }
}