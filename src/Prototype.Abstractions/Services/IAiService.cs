namespace DriftingBytesLabs.Prototype.Abstractions.Services;

public interface IAiService
{
    IAsyncEnumerable<string> QuestionAsync
    (
        string question
    );

    Task<Stream> TalkAsync
    (
        string text
    );
}