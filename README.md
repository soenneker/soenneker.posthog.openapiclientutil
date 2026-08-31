[![](https://img.shields.io/nuget/v/soenneker.posthog.openapiclientutil.svg?style=for-the-badge)](https://www.nuget.org/packages/soenneker.posthog.openapiclientutil/)
[![](https://img.shields.io/github/actions/workflow/status/soenneker/soenneker.posthog.openapiclientutil/publish-package.yml?style=for-the-badge)](https://github.com/soenneker/soenneker.posthog.openapiclientutil/actions/workflows/publish-package.yml)
[![](https://img.shields.io/nuget/dt/soenneker.posthog.openapiclientutil.svg?style=for-the-badge)](https://www.nuget.org/packages/soenneker.posthog.openapiclientutil/)
[![](https://img.shields.io/github/actions/workflow/status/soenneker/soenneker.posthog.openapiclientutil/codeql.yml?style=for-the-badge&label=codeql)](https://github.com/soenneker/soenneker.posthog.openapiclientutil/actions/workflows/codeql.yml)

# ![](https://user-images.githubusercontent.com/4441470/224455560-91ed3ee7-f510-4041-a8d2-3fc093025112.png) Soenneker.PostHog.OpenApiClientUtil

Provides a configured PostHog management client and reuses it for the lifetime of the registered service.

## Installation

```bash
dotnet add package Soenneker.PostHog.OpenApiClientUtil
```

## Configuration

```json
{
  "PostHog": {
    "ApiKey": "your-personal-api-key",
    "ClientBaseUrl": "https://us.posthog.com/"
  }
}
```

Use the account's regional host or self-hosted root URL. The key must be a personal API key with the scopes needed by the requested management endpoints.

## Usage

```csharp
using Soenneker.PostHog.OpenApiClientUtil.Abstract;
using Soenneker.PostHog.OpenApiClientUtil.Registrars;

services.AddPostHogOpenApiClientUtilAsSingleton();

IPostHogOpenApiClientUtil postHog = serviceProvider
    .GetRequiredService<IPostHogOpenApiClientUtil>();

var client = await postHog.Get(cancellationToken);
var organizations = await client.Api.Organizations.GetAsync(
    cancellationToken: cancellationToken);
```

Use `AddPostHogOpenApiClientUtilAsScoped()` when each application scope should have its own generated client wrapper. The authenticated HTTP provider remains shared and is disposed by the service container at shutdown.
