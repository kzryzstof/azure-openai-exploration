using CommunityToolkit.Diagnostics;
using DriftingBytesLabs.Prototype.Abstractions.Requests;
using DriftingBytesLabs.Prototype.Abstractions.Services;
using JetBrains.Annotations;
using Mediator;

namespace DriftingBytesLabs.Prototype.Application.Handlers;

[UsedImplicitly]
internal sealed class TalkHandler : IRequestHandler<TalkRequest, TalkResponse>
{
    private readonly IAiService _aiService;
    
    public TalkHandler
    (
        IAiService aiService
    )
    {
        Guard.IsNotNull(aiService);

        _aiService = aiService;
    }

    public async ValueTask<TalkResponse> Handle
    (
        TalkRequest request,
        CancellationToken cancellationToken
    )
    {
        return new TalkResponse
        (
            await _aiService.TalkAsync
            (
                request.Text
            )
        );
    }
}