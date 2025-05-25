using DriftingBytesLabs.Prototype.Application.Hosting.Extensions;
using DriftingBytesLabs.Prototype.Services.AzureOpenAI.Hosting.Extensions;

namespace DriftingBytesLabs.Prototype.Host.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddServices
    (
        this IServiceCollection services
    )
    {
        services
            .AddApplicationServices()
            .AddAzureOpenAiServices()
            ;
        
        return services;
    }
}