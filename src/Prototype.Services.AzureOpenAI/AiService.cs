using System.Text.Json;
using Azure;
using Azure.AI.OpenAI;
using DriftingBytesLabs.Prototype.Abstractions.Services;
using DriftingBytesLabs.Prototype.Services.AzureOpenAI.Hosting.Configurations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using OpenAI.Chat;

namespace DriftingBytesLabs.Prototype.Services.AzureOpenAI;

internal sealed class AiService : IAiService
{
    private static readonly ChatCompletionOptions RequestOptions = new ()
    {
        MaxOutputTokenCount = 1_000
    };

    private readonly ChatClient _chatClient;
    
    public AiService
    (
        IOptions<AzureOpenAiConfiguration> options,
        IConfiguration configuration
    )
    {
        var azureAiFoundryAccessKey = configuration[options.Value.AccessKeySecretName]!;
        
        var aiClient = new AzureOpenAIClient
        (
            new Uri(options.Value.Endpoint),
            //  https://github.com/Azure/azure-sdk-for-net/issues/49462
            //new DefaultAzureCredential(),
            new AzureKeyCredential(azureAiFoundryAccessKey)
        );
        
        _chatClient = aiClient.GetChatClient(options.Value.ChatDeploymentName);
    }

    public async IAsyncEnumerable<string> QuestionAsync
    (
        string question
    )
    {
        var chatUpdates = _chatClient.CompleteChatStreamingAsync
        (
    [
                new SystemChatMessage("You are Duke Nukem. The Duke. You take shit from no one. You are very friendly and use references from your world but you let everyone know who is the boss."),
                new UserChatMessage(question)
            ],
            RequestOptions
        );

        await foreach(StreamingChatCompletionUpdate? chatUpdate in chatUpdates)
        {
            foreach(ChatMessageContentPart? contentPart in chatUpdate.ContentUpdate)
            {
                yield return contentPart.Text;
            }
        }
    }
}