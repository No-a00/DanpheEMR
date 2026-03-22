namespace DanpheEMR.Application.Abstractions.Infrastructure
{
    public interface IEmailSender
    {
        Task SendEmailAsync(
            string to,
            string subject,
            string body,
            bool isHtml = true
            );
        Task SendBulleEmailAsync(
            IEnumerable<string> emails,
            string subject,
            string body);
                
            
          
    }
}
