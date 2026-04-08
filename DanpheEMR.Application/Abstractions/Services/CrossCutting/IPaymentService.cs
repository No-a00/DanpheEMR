namespace DanpheEMR.Application.Abstractions.Services.CrossCutting
{
    public interface IPaymentService
    {
        Task<string> CreatePaymentUrlAsync(Guid invoiceId, decimal amount, string description, CancellationToken cancellationToken = default);
        Task<bool> VerifyPaymentSignatureAsync(string data, string signature);
    }
}