using System.Text.Json.Serialization;

namespace DriftingBytesLabs.Prototype.Services.AzureOpenAI.Entities;

internal readonly record struct TextToSpeechContract
(
    [property: JsonPropertyName("model")]
    string Model,
    [property: JsonPropertyName("input")]
    string Text,
    [property: JsonPropertyName("voice")]
    string Voice
);