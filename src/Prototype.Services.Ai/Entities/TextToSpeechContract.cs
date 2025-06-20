using System.Text.Json.Serialization;

namespace DriftingBytesLabs.Prototype.Services.Ai.Entities;

internal readonly record struct TextToSpeechContract
(
    [property: JsonPropertyName("model")]
    string Model,
    [property: JsonPropertyName("input")]
    string Text,
    [property: JsonPropertyName("voice")]
    string Voice
);