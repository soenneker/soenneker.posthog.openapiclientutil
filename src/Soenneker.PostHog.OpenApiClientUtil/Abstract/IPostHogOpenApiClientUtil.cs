using Soenneker.PostHog.OpenApiClient;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Soenneker.PostHog.OpenApiClientUtil.Abstract;

/// <summary>
/// Provides a cached, authenticated client for PostHog's management API.
/// </summary>
public interface IPostHogOpenApiClientUtil: IDisposable, IAsyncDisposable
{
    /// <summary>
    /// Gets the generated client.
    /// </summary>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The cached PostHog client.</returns>
    ValueTask<PostHogOpenApiClient> Get(CancellationToken cancellationToken = default);
}
