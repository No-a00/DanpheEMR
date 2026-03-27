

namespace DanpheEMR.Application.Features.BloodBank.Queries.GetBloodInventory
{
    public class GetBloodInventoryResponse
    {
        public int TotalAvailableBags { get; set; }
        public List<BloodBagDto> Bags { get; set; } = new();
    }

    public class BloodBagDto
    {
        public Guid InventoryId { get; set; }
        public string BagNumber { get; set; }
        public string BloodGroupName { get; set; } 
        public int VolumeInMl { get; set; }
        public DateTime CollectionDate { get; set; }
        public DateTime ExpiryDate { get; set; }
        public int DaysUntilExpiry { get; set; }
    }
}