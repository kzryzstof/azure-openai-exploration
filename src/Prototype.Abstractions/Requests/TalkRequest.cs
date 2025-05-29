using Mediator;

namespace DriftingBytesLabs.Prototype.Abstractions.Requests;

public sealed record TalkRequest
(
    string Text
) : IRequest<TalkResponse>;