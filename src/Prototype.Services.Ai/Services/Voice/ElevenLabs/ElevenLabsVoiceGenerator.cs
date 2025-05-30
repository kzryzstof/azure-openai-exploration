using System.Net.Mime;
using System.Text;
using System.Text.Json;
using CommunityToolkit.Diagnostics;
using DriftingBytesLabs.Prototype.Services.Ai.Hosting.Configurations;
using Microsoft.Extensions.Options;

namespace DriftingBytesLabs.Prototype.Services.Ai.Services.Voice.ElevenLabs;

internal sealed class ElevenLabsVoiceGenerator : IVoiceGenerator
{
    private readonly HttpClient _httpClient;
    private readonly string _voiceId;
    
    public ElevenLabsVoiceGenerator
    (
        IOptions<ElevenLabsConfiguration> elevenLabsConfigurationOptions,
        IHttpClientFactory httpClientFactory
    )
    {
        Guard.IsNotNull(elevenLabsConfigurationOptions);
        Guard.IsNotNull(httpClientFactory);
        
        _voiceId = elevenLabsConfigurationOptions.Value.VoiceId;
        _httpClient = httpClientFactory.CreateClient(Constants.ElevenLabsHttpClient);
    }

    public async Task<Stream> TalkAsync
    (
        string text
    )
    {
        HttpResponseMessage response = await _httpClient.PostAsync
        (
            $"{_voiceId}?output_format=mp3_44100_128",
            new StringContent
            (
                JsonSerializer.Serialize(new CreateSpeechContract(text)),
                Encoding.UTF8,
                MediaTypeNames.Application.Json
            )
        );
        
        return await response.Content.ReadAsStreamAsync();
    }
}