using System.Net.Mime;
using System.Text;
using System.Text.Json;
using DriftingBytesLabs.Prototype.Services.Ai.Entities;
using DriftingBytesLabs.Prototype.Services.Ai.Hosting.Configurations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace DriftingBytesLabs.Prototype.Services.Ai.Services.Voice.Azure;

internal sealed class AzureVoiceGenerator : IVoiceGenerator
{
    private readonly AzureOpenAiConfiguration _azureOpenAiConfiguration;
    private readonly HttpClient _text2SpeecHttpClient;
    private readonly string _text2SpeechEndpoint;

    public AzureVoiceGenerator
    (
        IOptions<AzureOpenAiConfiguration> options,
        IConfiguration configuration,
        IHttpClientFactory httpClientFactory,
        IVoiceGenerator voiceGenerator
    )
    {
        _azureOpenAiConfiguration = options.Value;
        
        var accesskey = configuration[_azureOpenAiConfiguration.AccessKeySecretName]!;
        
        _text2SpeecHttpClient = httpClientFactory.CreateClient();
        _text2SpeecHttpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {accesskey}");
        _text2SpeechEndpoint = $"https://eastus2.cognitiveservices.azure.com/openai/deployments/{_azureOpenAiConfiguration.SpeechDeploymentName}/audio/speech?api-version=2025-03-01-preview";
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