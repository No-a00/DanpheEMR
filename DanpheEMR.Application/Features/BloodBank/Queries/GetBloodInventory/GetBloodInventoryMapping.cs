
using DomainBloodInventory = DanpheEMR.Core.Domain.BloodBank.BloodInventory;

namespace DanpheEMR.Application.Features.BloodBank.Queries.GetBloodInventory
{
    public static class GetBloodInventoryMapping
    {
        public static BloodBagDto ToDto(this DomainBloodInventory bag)
        {
            return new BloodBagDto
            {
                InventoryId = bag.Id,
                BagNumber = bag.BagNumber,
                VolumeInMl = bag.VolumeInMl,
                CollectionDate = bag.CollectionDate,
                ExpiryDate = bag.ExpiryDate,

                BloodGroupName = bag.BloodGroup != null ? bag.BloodGroup.BloodGroupName.ToString() : "N/A",

                DaysUntilExpiry = (bag.ExpiryDate.Date - DateTime.Today).Days
            };
        }

        public static List<BloodBagDto> ToDtoList(this IEnumerable<DomainBloodInventory> bags)
        {
            return bags.Select(b => b.ToDto()).ToList();
        }
    }
}