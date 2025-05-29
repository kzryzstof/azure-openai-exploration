namespace DriftingBytesLabs.Prototype.Services.AzureOpenAI.Hosting.Configurations;

public sealed class AzureOpenAiConfiguration
{
    public string Endpoint { get; init; } = string.Empty;
    public string ChatDeploymentName { get; init; } = string.Empty;
    public string SpeechDeploymentName { get; init; } = string.Empty;
    public string SpeechDeploymentModel { get; init; } = string.Empty;
    public string AccessKeySecretName { get; init; } = string.Empty;
}