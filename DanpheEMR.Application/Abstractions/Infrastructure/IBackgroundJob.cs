
using System.Linq.Expressions;

namespace DanpheEMR.Application.Abstractions.Infrastructure
{
    public interface IBackgroundJob
    {
        // Thực thi một task ngay lập tức (Fire-and-forget)
        string Enqueue(Expression<Action> methodCall);

        // Thực thi một task sau một khoảng thời gian delay
        string Schedule(Expression<Action> methodCall, TimeSpan delay);

        // Thực thi một task định kỳ (Cron job)
        void AddOrUpdate(string recurringJobId, Expression<Action> methodCall, string cronExpression);
    }
}