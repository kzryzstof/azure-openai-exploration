using DriftingBytesLabs.Prototype.Abstractions.Extensions;
using DriftingBytesLabs.Prototype.Abstractions.Services;
using DriftingBytesLabs.Prototype.Services.AzureOpenAI.Hosting.Configurations;
using Microsoft.Extensions.DependencyInjection;

namespace DriftingBytesLabs.Prototype.Services.AzureOpenAI.Hosting.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddAzureOpenAiServices
    (
        this IServiceCollection services
    )
    {
        //  Injects the configuration.
        services.AddConfiguration<AzureOpenAiConfiguration>();
        
        //  Injects the public services.
        services.AddSingleton<IAiService, AiService>();
        
        return services;
    }
}