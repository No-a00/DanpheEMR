using DanpheEMR.Core.Domain.BloodBank;
using DanpheEMR.Core.Enums;
using System;

namespace DanpheEMR.Application.Features.BloodBank.Commands.RecordDonation
{
    public static class RecordDonationMapping
    {
        public static BloodInventory ToInventoryEntity(this RecordDonationCommand command, BloodDonor donor)
        {
            return new BloodInventory
            {
                Id = Guid.NewGuid(),
                BagNumber = command.BagNumber,
                VolumeInMl = command.VolumeInMl,
                CollectionDate = DateTime.Now,
                
                ExpiryDate = DateTime.Now.AddDays(42),
                Status = BloodBagStatus.Available, 

                BloodGroupId = donor.BloodGroupId, 
                BloodDonorId = donor.Id
            };
        }
    }
}