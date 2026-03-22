
namespace DanpheEMR.Application.Abstractions.Services.CrossCutting
{
    public interface IIntegrationService
    {
        Task SendLabOrderAsync(
            Guid orderId);

        Task SyncInsuranceAsync(
            Guid patientId);
    }
}
