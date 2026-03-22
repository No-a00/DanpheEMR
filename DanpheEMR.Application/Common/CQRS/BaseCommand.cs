using MediatR;

public abstract class BaseCommand<TResponse>
    : IRequest<TResponse>
{
}