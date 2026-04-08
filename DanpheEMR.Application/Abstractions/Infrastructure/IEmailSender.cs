using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace DanpheEMR.Application.Abstractions.Infrastructure
{
    public interface IEmailSender
    {
        Task SendEmailAsync(string to, string subject, string body, bool isHtml = true, CancellationToken cancellationToken = default);
        Task SendEmailWithAttachmentsAsync(string to, string subject, string body, IEnumerable<string> attachmentPaths, CancellationToken cancellationToken = default);
    }
}