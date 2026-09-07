using Microsoft.Extensions.Logging;
using Netwise.RecruitmentTask.Execution.Abstractions;
using Netwise.RecruitmentTask.Services.Abstractions;

namespace Netwise.RecruitmentTask.Execution;

public class ProcessCatFactHandler(
    ICatFactClient catFactClient,
    IFileWriter fileWriter,
    ILogger<ProcessCatFactHandler> logger) : IProcessCatFactHandler
{
    public async Task HandleAsync(CancellationToken cancellationToken = default)
    {
        var fact = await catFactClient.GetCatFactAsync(cancellationToken);

        if (fact == null)
        {
            logger.LogWarning("No cat fact has been retrieved. Processing aborted.");
            return;
        }

        await fileWriter.WriteFactAsync(fact, cancellationToken);
    }
}
