using System.Text.Json.Serialization;

namespace DriftingBytesLabs.Prototype.Services.Ai.Services.Voice.ElevenLabs;

internal readonly record struct CreateSpeechContract
(
    [property: JsonPropertyName("text")]
    string Text,
    [property: JsonPropertyName("model_id")]
    string ModelId = "eleven_multilingual_v2"
);