using Soenneker.PostHog.OpenApiClientUtil.Abstract;
using Soenneker.TestHosts.Unit;
using Soenneker.Tests.HostedUnit;

namespace Soenneker.PostHog.OpenApiClientUtil.Tests;

[ClassDataSource<Host>(Shared = SharedType.PerTestSession)]
public sealed class PostHogOpenApiClientUtilTests : HostedUnitTest
{
    private readonly IPostHogOpenApiClientUtil _openapiclientutil;

    public PostHogOpenApiClientUtilTests(UnitTestHost host) : base(host)
    {
        _openapiclientutil = Resolve<IPostHogOpenApiClientUtil>(true);
    }

    [Test]
    public void Default()
    {

    }
}
