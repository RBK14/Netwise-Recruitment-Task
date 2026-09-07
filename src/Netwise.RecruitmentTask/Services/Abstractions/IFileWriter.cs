using Netwise.RecruitmentTask.Models.Domain;

namespace Netwise.RecruitmentTask.Services.Abstractions;

public interface IFileWriter
{
    Task WriteFactAsync(CatFact fact, CancellationToken cancellationToken = default);
}
