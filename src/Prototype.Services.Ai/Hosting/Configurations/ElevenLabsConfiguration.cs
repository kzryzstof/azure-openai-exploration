namespace DriftingBytesLabs.Prototype.Services.Ai.Hosting.Configurations;

internal sealed class ElevenLabsConfiguration
{
    public string ApiKey { get; init; } = string.Empty;
    public string EndpointUrl { get; init; } = string.Empty;
    public string VoiceId { get; init; } = string.Empty;
}