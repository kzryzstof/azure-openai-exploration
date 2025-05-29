using System.Net.Mime;
using System.Text;
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
    
    private readonly AzureOpenAiConfiguration _azureOpenAiConfiguration;
    private readonly HttpClient _text2SpeecHttpClient;
    private readonly string _text2SpeechEndpoint;
    
    public AiService
    (
        IOptions<AzureOpenAiConfiguration> options,
        IConfiguration configuration,
        IHttpClientFactory httpClientFactory
    )
    {
        _azureOpenAiConfiguration = options.Value;
        
        var accesskey = configuration[_azureOpenAiConfiguration.AccessKeySecretName]!;
        
        var aiClient = new AzureOpenAIClient
        (
            new Uri(options.Value.Endpoint),
            //  https://github.com/Azure/azure-sdk-for-net/issues/49462
            //new DefaultAzureCredential(),
            new AzureKeyCredential(accesskey)
        );
        
        _chatClient = aiClient.GetChatClient(_azureOpenAiConfiguration.ChatDeploymentName);
        
        _text2SpeecHttpClient = httpClientFactory.CreateClient();
        _text2SpeecHttpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {accesskey}");
        
        _text2SpeechEndpoint = $"https://eastus2.cognitiveservices.azure.com/openai/deployments/{_azureOpenAiConfiguration.SpeechDeploymentName}/audio/speech?api-version=2025-03-01-preview";
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

    public async Task<Stream> TalkAsync
    (
        string text
    )
    {
        HttpContent content = new StringContent
        (
            JsonSerializer.Serialize
            (
                new TextToSpeechContract
                (
                    _azureOpenAiConfiguration.SpeechDeploymentModel,
                    text,
                    "alloy"
                )
            ),
            Encoding.UTF8,
            MediaTypeNames.Application.Json
        );

        HttpResponseMessage response = await _text2SpeecHttpClient.PostAsync
        (
            _text2SpeechEndpoint,
            content
        );
        
        return await response.Content.ReadAsStreamAsync();
    }
}