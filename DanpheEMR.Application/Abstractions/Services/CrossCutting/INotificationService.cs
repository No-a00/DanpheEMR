

namespace DanpheEMR.Application.Abstractions.Services.CrossCutting
{
    public interface INotificationService
    {
        Task SendEmailAsync(string email,string subject,string body);
        Task SendSmsAsync(string phone,string message);
        Task SendSystemNotificationAsync(Guid userId, string message);
    }
}
