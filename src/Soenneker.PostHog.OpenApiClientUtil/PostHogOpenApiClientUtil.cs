using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Kiota.Http.HttpClientLibrary;
using Soenneker.Extensions.Configuration;
using Soenneker.Extensions.ValueTask;
using Soenneker.PostHog.HttpClients.Abstract;
using Soenneker.PostHog.OpenApiClientUtil.Abstract;
using Soenneker.PostHog.OpenApiClient;
using Soenneker.Kiota.GenericAuthenticationProvider;
using Soenneker.Utils.AsyncSingleton;

namespace Soenneker.PostHog.OpenApiClientUtil;

///<inheritdoc cref="IPostHogOpenApiClientUtil"/>
public sealed class PostHogOpenApiClientUtil : IPostHogOpenApiClientUtil
{
    private readonly AsyncSingleton<PostHogOpenApiClient> _client;

    public PostHogOpenApiClientUtil(IPostHogOpenApiHttpClient httpClientUtil, IConfiguration configuration)
    {
        _client = new AsyncSingleton<PostHogOpenApiClient>(async token =>
        {
            HttpClient httpClient = await httpClientUtil.Get(token).NoSync();

            var apiKey = configuration.GetValueStrict<string>("PostHog:ApiKey");
            string authHeaderValueTemplate = configuration["PostHog:AuthHeaderValueTemplate"] ?? "Bearer {token}";
            string authHeaderValue = authHeaderValueTemplate.Replace("{token}", apiKey, StringComparison.Ordinal);

            var requestAdapter = new HttpClientRequestAdapter(new GenericAuthenticationProvider(headerValue: authHeaderValue), httpClient: httpClient);

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
