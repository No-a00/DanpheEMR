using DanpheEMR.Core.Domain.BloodBank;
using System;

namespace DanpheEMR.Application.Features.BloodBank.Commands.IssueBlood
{
    public static class IssueBloodMapping
    {
        public static BloodIssue ToIssueEntity(this IssueBloodCommand command, BloodInventory bloodBag, Guid currentUserId)
        {
            return new BloodIssue
            {
                Id = Guid.NewGuid(),
                IssueDate = DateTime.Now,
                Remarks = command.Remarks,
                IssuedByUserId = currentUserId,
                PatientId = command.PatientId,
                BloodInventoryId = bloodBag.Id 
            };
        }
    }
}