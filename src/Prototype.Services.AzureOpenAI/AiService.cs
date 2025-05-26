using Azure.AI.OpenAI;
using Azure.Identity;
using DriftingBytesLabs.Prototype.Abstractions.Services;
using DriftingBytesLabs.Prototype.Services.AzureOpenAI.Hosting.Configurations;
using Microsoft.Extensions.Options;
using OpenAI.Chat;

namespace DriftingBytesLabs.Prototype.Services.AzureOpenAI;

internal sealed class AiService : IAiService
{
    private readonly AzureOpenAIClient _aiClient;
    private readonly AzureOpenAiConfiguration _configuration;
    
    public AiService
    (
        IOptions<AzureOpenAiConfiguration> options
    )
    {
        _configuration = options.Value;
        
        _aiClient = new AzureOpenAIClient
        (
            new Uri(_configuration.Endpoint),
            new DefaultAzureCredential()
        );
    }

    public async Task TestAsync
    ( )
    {
        ChatClient chatClient = _aiClient.GetChatClient(_configuration.DeploymentName);

        var chatUpdates = chatClient.CompleteChatStreamingAsync
        (
        [
                new SystemChatMessage("You are a helpful assistant that talks like a pirate."),
                new UserChatMessage("Does Azure OpenAI support customer managed keys?"),
                new AssistantChatMessage("Yes, customer managed keys are supported by Azure OpenAI"),
                new UserChatMessage("Do other Azure services support this too?")
            ]
        );

        await foreach(var chatUpdate in chatUpdates)
        {
            if (chatUpdate.Role.HasValue)
            {
                Console.Write($"{chatUpdate.Role} : ");
            }

            foreach(var contentPart in chatUpdate.ContentUpdate)
            {
                Console.Write(contentPart.Text);
            }
        }
    }
}