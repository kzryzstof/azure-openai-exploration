using DriftingBytesLabs.Prototype.Application.Hosting.Extensions;
using DriftingBytesLabs.Prototype.Services.Ai.Hosting.Extensions;

namespace DriftingBytesLabs.Prototype.Host.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddServices
    (
        this IServiceCollection services,
        IConfiguration configuration
    )
    {
        services
            .AddApplicationServices()
            .AddAzureOpenAiServices(configuration)
            ;
        
        return services;
    }
}