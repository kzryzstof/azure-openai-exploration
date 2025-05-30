using DriftingBytesLabs.Prototype.Abstractions.Extensions;
using DriftingBytesLabs.Prototype.Abstractions.Services;
using DriftingBytesLabs.Prototype.Services.Ai.Hosting.Configurations;
using DriftingBytesLabs.Prototype.Services.Ai.Services;
using DriftingBytesLabs.Prototype.Services.Ai.Services.Voice.ElevenLabs;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace DriftingBytesLabs.Prototype.Services.Ai.Hosting.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddAzureOpenAiServices
    (
        this IServiceCollection services,
        IConfiguration configuration
    )
    {
        //  Injects the configuration.
        services.AddConfiguration<AzureOpenAiConfiguration>();
        services.AddConfiguration<ElevenLabsConfiguration>();
        
        //  Injects internal services.
        services.AddHttpClient();
        services.AddHttpClient<ElevenLabsConfiguration>
        (
            Constants.ElevenLabsHttpClient,
            (serviceProvider, httpClient) =>
            {
                var elevenLabsConfigurationOptions = serviceProvider.GetRequiredService<IOptions<ElevenLabsConfiguration>>();
                httpClient.BaseAddress = new Uri(elevenLabsConfigurationOptions.Value.EndpointUrl);
                httpClient.DefaultRequestHeaders.Add("xi-api-key", elevenLabsConfigurationOptions.Value.ApiKey);
            });
        
        
        services.AddSingleton<IVoiceGenerator, ElevenLabsVoiceGenerator>();

        //  Injects the public services.
        services.AddSingleton<IAiService, AiService>();
        
        return services;
    }
}