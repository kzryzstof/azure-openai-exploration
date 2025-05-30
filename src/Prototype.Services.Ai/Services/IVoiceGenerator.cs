namespace DriftingBytesLabs.Prototype.Services.Ai.Services;

internal interface IVoiceGenerator
{
    Task<Stream> TalkAsync
    (
        string text
    );
}