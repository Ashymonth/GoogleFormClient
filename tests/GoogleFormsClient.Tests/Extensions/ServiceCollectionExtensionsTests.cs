using GoogleFormsClient.Extensions;
using Microsoft.Extensions.DependencyInjection;

namespace GoogleFormsClient.Tests.Extensions;

public class ServiceCollectionExtensionsTests
{
    [Fact]
    public void AddGoogleFormsClientTest_Should_Resolve_Client()
    {
        var provider = new ServiceCollection().AddGoogleFormsClient().BuildServiceProvider();
    }
}