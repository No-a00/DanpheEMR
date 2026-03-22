namespace Application.Abstractions.Infrastructure;

public interface IGuidGenerator
{
    Guid NewGuid();
}