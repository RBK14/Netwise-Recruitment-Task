using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Netwise.RecruitmentTask.Configuration;
using Netwise.RecruitmentTask.Models.Domain;
using Netwise.RecruitmentTask.Models.Dtos;
using Netwise.RecruitmentTask.Services.Abstractions;
using System.Net.Http.Json;

namespace Netwise.RecruitmentTask.Services;

public class CatFactClient(
    HttpClient httpClient,
    IOptions<ApiSettings> apiSettings,
    ILogger<CatFactClient> logger) : ICatFactClient
{
    private readonly ApiSettings _apiSettings = apiSettings.Value;

    public async Task<CatFact?> GetCatFactAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            var responseDto = await httpClient.GetFromJsonAsync<CatFactDto>(
                _apiSettings.CatFactEndpoint,
                cancellationToken);

            if (responseDto is null || string.IsNullOrWhiteSpace(responseDto.Fact))
            {
                logger.LogWarning("No cat fact available. Received null or empty response from API.");
                return null;
            }

            return new CatFact(responseDto.Fact);
        }
        catch (HttpRequestException ex)
        {
            logger.LogError(ex, "HTTP request failed while fetching.");
            return null;
        }
        catch(Exception ex)
        {
            logger.LogError(ex, "Unexpected error occurred while fetching.");
            throw;
        }
    }
}
