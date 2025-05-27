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
    private readonly AzureOpenAIClient _aiClient;
    private readonly AzureOpenAiConfiguration _configuration;
    
    public AiService
    (
        IOptions<AzureOpenAiConfiguration> options,
        IConfiguration configuration
    )
    {
        _configuration = options.Value;

        var azureAiFoundryKey = JsonSerializer.Deserialize<AzureAiFoundryKey>(configuration[_configuration.SecretKey]!);
        
        _aiClient = new AzureOpenAIClient
        (
            new Uri(_configuration.Endpoint),
            //  https://github.com/Azure/azure-sdk-for-net/issues/49462
            //new DefaultAzureCredential(),
            new AzureKeyCredential(azureAiFoundryKey.Key)
        );
    }

    public async Task TestAsync()
    {
        ChatClient chatClient = _aiClient.GetChatClient(_configuration.DeploymentName);

        var requestOptions = new ChatCompletionOptions
        {
            MaxOutputTokenCount = 1_000
        };
        
        var chatUpdates = chatClient.CompleteChatStreamingAsync
        (
    [
                new SystemChatMessage("You are a helpful assistant."),
                new UserChatMessage("I am going to Paris, what should I see?"),
                new AssistantChatMessage("Yes, customer managed keys are supported by Azure OpenAI"),
                new UserChatMessage("Do other Azure services support this too?")
            ],
            requestOptions
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