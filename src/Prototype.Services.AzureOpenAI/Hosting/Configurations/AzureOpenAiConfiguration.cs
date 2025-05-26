namespace DriftingBytesLabs.Prototype.Services.AzureOpenAI.Hosting.Configurations;

public sealed class AzureOpenAiConfiguration
{
    public string Endpoint { get; init; } = string.Empty;
    public string DeploymentName { get; init; } = string.Empty;
    public string OpenAiVersion { get; init; } = string.Empty;
}