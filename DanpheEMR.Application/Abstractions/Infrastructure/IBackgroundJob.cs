namespace Application.Abstractions.Infrastructure;

public interface IBackgroundJob
{
    void Enqueue(
        Func<Task> job);

    void Schedule(
        Func<Task> job,
        TimeSpan delay);
}