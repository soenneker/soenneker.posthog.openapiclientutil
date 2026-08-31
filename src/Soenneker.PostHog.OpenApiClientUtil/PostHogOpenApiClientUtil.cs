using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Kiota.Abstractions.Authentication;
using Microsoft.Kiota.Http.HttpClientLibrary;
using Soenneker.Extensions.ValueTask;
using Soenneker.PostHog.HttpClients.Abstract;
using Soenneker.PostHog.OpenApiClientUtil.Abstract;
using Soenneker.PostHog.OpenApiClient;
using Soenneker.Utils.AsyncSingleton;

namespace Soenneker.PostHog.OpenApiClientUtil;

public sealed class PostHogOpenApiClientUtil : IPostHogOpenApiClientUtil
{
    private readonly AsyncSingleton<PostHogOpenApiClient> _client;

    public PostHogOpenApiClientUtil(IPostHogOpenApiHttpClient httpClientUtil)
    {
        _client = new AsyncSingleton<PostHogOpenApiClient>(async token =>
        {
            HttpClient httpClient = await httpClientUtil.Get(token).NoSync();

            var requestAdapter = new HttpClientRequestAdapter(new AnonymousAuthenticationProvider(), httpClient: httpClient)
            {
                BaseUrl = httpClient.BaseAddress!.ToString().TrimEnd('/')
            };

            return new PostHogOpenApiClient(requestAdapter);
        });
    }

    public ValueTask<PostHogOpenApiClient> Get(CancellationToken cancellationToken = default)
    {
        return _client.Get(cancellationToken);
    }

    public void Dispose()
    {
        _client.Dispose();
    }

    public ValueTask DisposeAsync()
    {
        return _client.DisposeAsync();
    }
}
