using DanpheEMR.Core.Domain.BloodBank;
using System;

namespace DanpheEMR.Application.Features.BloodBank.Commands.RegisterDonor
{
    public static class RegisterDonorMapping
    {
        public static BloodDonor ToEntity(this RegisterDonorCommand command)
        {
            return new BloodDonor
            {
                Id = Guid.NewGuid(), // Tạo ID mới
                DonorName = command.DonorName,
                Contact = command.Contact,
                DateOfBirth = command.DateOfBirth,
                Gender = command.Gender,
                Weight = command.Weight,
                BloodGroupId = command.BloodGroupId,
                TotalDonations = 0,
                IsPermanentlyDeferred = false,
                IsDeleted = false
            };
        }
    }
}