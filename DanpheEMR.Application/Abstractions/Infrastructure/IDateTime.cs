
namespace DanpheEMR.Application.Abstractions.Infrastructure
{
    public interface IDateTime
    {
        DateTime Now { get; }
        DateTime UtcNow { get; }
        DateTime Today { get; }

    }
}
