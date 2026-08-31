using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Soenneker.PostHog.HttpClients.Registrars;
using Soenneker.PostHog.OpenApiClientUtil.Abstract;

namespace Soenneker.PostHog.OpenApiClientUtil.Registrars;

/// <summary>
/// Registers the PostHog management API client provider.
/// </summary>
public static class PostHogOpenApiClientUtilRegistrar
{
    /// <summary>
    /// Adds <see cref="PostHogOpenApiClientUtil"/> as a singleton service. <para/>
    /// </summary>
    public static IServiceCollection AddPostHogOpenApiClientUtilAsSingleton(this IServiceCollection services)
    {
        services.AddPostHogOpenApiHttpClientAsSingleton()
                .TryAddSingleton<IPostHogOpenApiClientUtil, PostHogOpenApiClientUtil>();

        return services;
    }

    /// <summary>
    /// Adds <see cref="PostHogOpenApiClientUtil"/> as a scoped service. <para/>
    /// </summary>
    public static IServiceCollection AddPostHogOpenApiClientUtilAsScoped(this IServiceCollection services)
    {
        services.AddPostHogOpenApiHttpClientAsSingleton()
                .TryAddScoped<IPostHogOpenApiClientUtil, PostHogOpenApiClientUtil>();

        return services;
    }
}
