namespace DriftingBytesLabs.Prototype.Abstractions.Requests;

public readonly record struct AskQuestionResponse
(
    IAsyncEnumerable<string> Response
);