using Mediator;

namespace DriftingBytesLabs.Prototype.Abstractions.Requests;

public sealed record AskQuestionRequest
(
    string Question
) : IRequest<AskQuestionResponse>;