using Azure;
using Azure.AI.OpenAI;
using Azure.AI.OpenAI.Chat;
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
            //  https://github.com/Azure/azure-sdk-for-net/issues/49462
            //new DefaultAzureCredential(),
            new AzureKeyCredential(_configuration.Secret),
            new AzureOpenAIClientOptions(AzureOpenAIClientOptions.ServiceVersion.V2025_03_01_Preview)
        );
    }

    public async Task TestAsync
    ( )
    {
        ChatClient chatClient = _aiClient.GetChatClient(_configuration.DeploymentName);

        var requestOptions = new ChatCompletionOptions
        {
            MaxOutputTokenCount = 1_000
        };
        
        // The SetNewMaxCompletionTokensPropertyEnabled() method is an [Experimental] opt-in to use
        // the new max_completion_tokens JSON property instead of the legacy max_tokens property.
        // This extension method will be removed and unnecessary in a future service API version;
        // please disable the [Experimental] warning to acknowledge.
#pragma warning disable AOAI001
        requestOptions.SetNewMaxCompletionTokensPropertyEnabled(true);
#pragma warning restore AOAI001
        
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