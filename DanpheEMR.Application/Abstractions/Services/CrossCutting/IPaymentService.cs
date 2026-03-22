
namespace DanpheEMR.Application.Abstractions.Services.CrossCutting
{
    public interface IPaymentService
    {
        Task<bool> ProcesspaymentAsync(Guid invoiceId, decimal amount);
        Task<bool> ReFundAsync(Guid invoiceId);
    }
}
