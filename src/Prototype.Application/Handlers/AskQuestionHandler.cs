using CommunityToolkit.Diagnostics;
using DriftingBytesLabs.Prototype.Abstractions.Requests;
using DriftingBytesLabs.Prototype.Abstractions.Services;
using JetBrains.Annotations;
using Mediator;

namespace DriftingBytesLabs.Prototype.Application.Handlers;

[UsedImplicitly]
internal sealed class AskQuestionHandler : IRequestHandler<AskQuestionRequest, AskQuestionResponse>
{
    private readonly IAiService _aiService;
    
    public AskQuestionHandler
    (
        IAiService aiService
    )
    {
        Guard.IsNotNull(aiService);

        _aiService = aiService;
    }

    public ValueTask<AskQuestionResponse> Handle
    (
        AskQuestionRequest request,
        CancellationToken cancellationToken
    )
    {
        return ValueTask.FromResult
        (
            new AskQuestionResponse
            (
                _aiService.QuestionAsync
                (
                    request.Question
                )
            )
        );
    }
}