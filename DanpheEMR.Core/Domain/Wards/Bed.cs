using DanpheEMR.Core.Domain.Base;
using DanpheEMR.Core.Domain.Nums;

namespace DanpheEMR.Core.Domain.Wards
{
    public class Bed : BaseEntity 
    {
        public int Id { get; set; }
        public string BedNumber { get; set; }
        public bool IsOccupied { get; set; } = false;
        public BedStatus Status { get; set; } 
        public int WardsId { get; set; }
         public Ward Wards { get; set; } 

        public int BedFeatureId { get; set; }
        public BedFeature BedFeature { get; set; } 
    }
}