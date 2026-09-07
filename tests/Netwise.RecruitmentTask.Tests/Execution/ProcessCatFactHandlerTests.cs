using Microsoft.Extensions.Logging;
using Moq;
using Netwise.RecruitmentTask.Execution;
using Netwise.RecruitmentTask.Models.Domain;
using Netwise.RecruitmentTask.Services.Abstractions;

namespace Netwise.RecruitmentTask.Tests.Execution;

public class ProcessCatFactHandlerTests
{
    private readonly Mock<ICatFactClient> _catFactClientMock;
    private readonly Mock<IFileWriter> _fileWriterMock;
    private readonly Mock<ILogger<ProcessCatFactHandler>> _loggerMock;
    private readonly ProcessCatFactHandler _handler;

    public ProcessCatFactHandlerTests()
    {
        _catFactClientMock = new Mock<ICatFactClient>();
        _fileWriterMock = new Mock<IFileWriter>();
        _loggerMock = new Mock<ILogger<ProcessCatFactHandler>>();

        _handler = new ProcessCatFactHandler(
            _catFactClientMock.Object,
            _fileWriterMock.Object,
            _loggerMock.Object);
    }

    [Fact]
    public async Task HandleAsync_WhenApiReturnsFact_ShouldCallFileWriter()
    {
        var expectedFact = new CatFact("This is an example cat fact.");
        _catFactClientMock
            .Setup(client => client.GetCatFactAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(expectedFact);

        await _handler.HandleAsync();

        _fileWriterMock
            .Verify(writer => writer.WriteFactAsync(expectedFact, It.IsAny<CancellationToken>()),
            Times.Once);
    }

    [Fact]
    public async Task HandleAsync_WhenApiReturnsNull_ShouldNotCallFileWriter()
    {
        _catFactClientMock
            .Setup(client => client.GetCatFactAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync((CatFact?)null);

        await _handler.HandleAsync();

        _fileWriterMock.Verify(
            writer => writer.WriteFactAsync(It.IsAny<CatFact>(), It.IsAny<CancellationToken>()),
            Times.Never);
    }
}
