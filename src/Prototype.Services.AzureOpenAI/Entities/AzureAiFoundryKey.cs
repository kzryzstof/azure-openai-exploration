using System.Text.Json.Serialization;

namespace DriftingBytesLabs.Prototype.Services.AzureOpenAI.Entities;

internal readonly record struct AzureAiFoundryKey
(
    [property: JsonPropertyName("Key")]
    string Key
);