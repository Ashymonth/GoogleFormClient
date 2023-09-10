using GoogleFormClient.Extensions;
using Microsoft.Extensions.DependencyInjection;

namespace GoogleFormClient.Tests.Extensions;

public class ServiceCollectionExtensionsTests
{
    [Fact]
    public void AddGoogleFormsClientTest_Should_Resolve_Client()
    {
        var provider = new ServiceCollection().AddGoogleFormsClient().BuildServiceProvider();
    }
}