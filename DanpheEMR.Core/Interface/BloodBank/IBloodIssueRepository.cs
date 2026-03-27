using DanpheEMR.Core.Domain.BloodBank;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DanpheEMR.Core.Interface.BloodBank
{
    public interface IBloodIssueRepository
    {
        Task AddAsync(BloodIssue issueRecord);

        Task<BloodIssue?> GetByIdAsync(Guid id);

        Task<IEnumerable<BloodIssue>> GetIssuesByPatientAsync(Guid patientId);
    }
}