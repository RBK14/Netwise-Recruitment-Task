namespace Netwise.RecruitmentTask.Execution.Abstractions;

public interface IProcessCatFactHandler
{
    Task HandleAsync(CancellationToken cancellationToken = default);
}
