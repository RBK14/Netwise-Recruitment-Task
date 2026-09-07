using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Netwise.RecruitmentTask.Configuration;
using Netwise.RecruitmentTask.Models.Domain;
using Netwise.RecruitmentTask.Services.Abstractions;
using System.Text;

namespace Netwise.RecruitmentTask.Services;

public class FileWriter(
    IOptions<FileSettings> fileSettings,
    ILogger<FileWriter> logger) : IFileWriter
{
    private readonly FileSettings _fileSettings = fileSettings.Value;

    public async Task WriteFactAsync(CatFact fact, CancellationToken cancellationToken = default)
    {
        try
        {
            var directoryPath = _fileSettings.OutputDirectory;
            var filePath = Path.Combine(directoryPath, _fileSettings.OutputFileName);

            if (!Directory.Exists(directoryPath))
                Directory.CreateDirectory(directoryPath);

            var content = $"{fact.Fact}{Environment.NewLine}";
            await File.AppendAllTextAsync(filePath, content, Encoding.UTF8, cancellationToken);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An error occurred while writing the cat fact to the file.");
            throw;
        }
    }
}
