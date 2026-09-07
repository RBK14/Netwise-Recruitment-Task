using Netwise.RecruitmentTask.Models.Domain;

namespace Netwise.RecruitmentTask.Services.Abstractions;

public interface ICatFactClient
{
    Task<CatFact?> GetCatFactAsync(CancellationToken cancellationToken = default);
}
