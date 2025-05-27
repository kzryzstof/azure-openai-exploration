using System.Text.Json;
using Azure;
using Azure.AI.OpenAI;
using DriftingBytesLabs.Prototype.Abstractions.Services;
using DriftingBytesLabs.Prototype.Services.AzureOpenAI.Entities;
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
        AzureOpenAiConfiguration configuration1 = options.Value;

        var azureAiFoundryKey = JsonSerializer.Deserialize<AzureAiFoundryKey>(configuration[configuration1.SecretKey]!);
        
        var aiClient = new AzureOpenAIClient
        (
            new Uri(configuration1.Endpoint),
            //  https://github.com/Azure/azure-sdk-for-net/issues/49462
            //new DefaultAzureCredential(),
            new AzureKeyCredential(azureAiFoundryKey.Key)
        );
        
        _chatClient = aiClient.GetChatClient(configuration1.ChatDeploymentName);
    }

    public async IAsyncEnumerable<string> QuestionAsync()
    {
        var chatUpdates = _chatClient.CompleteChatStreamingAsync
        (
    [
                new SystemChatMessage("You are a helpful assistant. You talk like Nintendo's character Mario. You are very friendly and use references from your world."),
                new UserChatMessage("I am going to Paris, what should I see?")
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