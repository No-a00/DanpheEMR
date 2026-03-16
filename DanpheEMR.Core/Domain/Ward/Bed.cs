using DanpheEMR.Core.Domain.Base;
using DanpheEMR.Core.Domain.Nums;

namespace DanpheEMR.Core.Domain.Ward
{
    public class Bed : BaseEntity 
    {
        public int Id { get; set; }
        public string BedNumber { get; set; }
        public bool IsOccupied { get; set; } = false;
        public BedStatus Status { get; set; } 
        public int WardId { get; set; }
         public Ward Ward { get; set; } 

        public int BedFeatureId { get; set; }
        public BedFeature BedFeature { get; set; } 
    }
}