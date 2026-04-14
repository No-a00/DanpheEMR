namespace DanpheEMR.Core.Interfaces.Base;
public interface ICurrentUserService
{
    Guid UserId { get; }
    string? UserName { get; }
    string? IpAddress { get; }
    string? CorrelationId { get; }
}