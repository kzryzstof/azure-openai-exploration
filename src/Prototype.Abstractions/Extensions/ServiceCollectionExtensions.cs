using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DriftingBytesLabs.Prototype.Abstractions.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddConfiguration<TConfigurationType>
    (
        this IServiceCollection services
    ) where TConfigurationType : class
    {
        services
            .AddOptions<TConfigurationType>()
            .Configure<IConfiguration>
            (
                (
                    settings,
                    configuration
                ) =>
                {
                    configuration.GetSection(typeof(TConfigurationType).Name).Bind(settings);
                }
            );
    }
}