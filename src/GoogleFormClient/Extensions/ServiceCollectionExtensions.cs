using GoogleFormClient.Parsers;
using Microsoft.Extensions.DependencyInjection;

namespace GoogleFormClient.Extensions;

/// <summary>
/// Extensions to add google forms client.
/// </summary>
public static class ServiceCollectionExtensions
{
    private const string BaseUrl = "https://docs.google.com";

    /// <summary>
    /// Add google form client. After that inject <see cref="IGoogleFormClient"/> in your services
    /// or create <see cref="GoogleFormClient"/> directly.
    /// </summary>
    /// <param name="services">Your services.</param>
    /// <returns></returns>
    public static IServiceCollection AddGoogleFormsClient(this IServiceCollection services)
    {
        services.AddHttpClient<IGoogleFormClient, GoogleFormClient>()
            .ConfigureHttpClient(client => client.BaseAddress = new Uri(BaseUrl));

        services.AddSingleton<IGoogleFormParser, GoogleFormParser>();

        return services;
    }
}