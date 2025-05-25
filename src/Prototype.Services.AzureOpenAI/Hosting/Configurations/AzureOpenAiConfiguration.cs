namespace DriftingBytesLabs.Prototype.Services.AzureOpenAI.Hosting.Configurations;

internal readonly record struct AzureOpenAiConfiguration
(
    string Endpoint,
    string DeploymentName,
    string OpenAiVersion = ""
);