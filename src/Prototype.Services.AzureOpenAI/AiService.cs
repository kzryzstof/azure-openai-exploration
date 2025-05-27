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
    
    private readonly AzureOpenAiConfiguration _configuration;
    private readonly ChatClient _chatClient;
    
    public AiService
    (
        IOptions<AzureOpenAiConfiguration> options,
        IConfiguration configuration
    )
    {
        _configuration = options.Value;

        var azureAiFoundryKey = JsonSerializer.Deserialize<AzureAiFoundryKey>(configuration[_configuration.SecretKey]!);
        
        var aiClient = new AzureOpenAIClient
        (
            new Uri(_configuration.Endpoint),
            //  https://github.com/Azure/azure-sdk-for-net/issues/49462
            //new DefaultAzureCredential(),
            new AzureKeyCredential(azureAiFoundryKey.Key)
        );
        
        _chatClient = aiClient.GetChatClient(_configuration.DeploymentName);
    }

    public async Task QuestionAsync()
    {
        var chatUpdates = _chatClient.CompleteChatStreamingAsync
        (
    [
                new SystemChatMessage("You are a helpful assistant. You talk like Nintendo's character Mario. You are very friendly and use references from your world."),
                new UserChatMessage("I am going to Paris, what should I see?"),
                //new AssistantChatMessage("Yes, customer managed keys are supported by Azure OpenAI"),
                //new UserChatMessage("Do other Azure services support this too?")
            ],
            RequestOptions
        );

        await foreach(StreamingChatCompletionUpdate? chatUpdate in chatUpdates)
        {
            if (chatUpdate.Role.HasValue)
            {
                Console.Write($"{chatUpdate.Role} : ");
            }

            foreach(ChatMessageContentPart? contentPart in chatUpdate.ContentUpdate)
            {
                Console.Write(contentPart.Text);
            }
        }
    }
}