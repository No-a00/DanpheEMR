using DanpheEMR.Core.Domain.BloodBank;
using DanpheEMR.Core.Interface.Base;
namespace DanpheEMR.Core.Interface.BloodBank
{
    public interface IBloodIssueRepository : IGenericRepository<BloodIssue>
    {
        Task<IEnumerable<BloodIssue>> GetIssuesByPatientAsync(Guid patientId);
    }
}