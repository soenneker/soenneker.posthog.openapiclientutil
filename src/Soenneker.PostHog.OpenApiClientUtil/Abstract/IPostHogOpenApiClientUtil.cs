using Soenneker.PostHog.OpenApiClient;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Soenneker.PostHog.OpenApiClientUtil.Abstract;

/// <summary>
/// Exposes a cached OpenAPI client instance.
/// </summary>
public interface IPostHogOpenApiClientUtil: IDisposable, IAsyncDisposable
{
    ValueTask<PostHogOpenApiClient> Get(CancellationToken cancellationToken = default);
}
