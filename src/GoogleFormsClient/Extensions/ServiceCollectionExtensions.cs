using GoogleFormsClient.Parsers;
using Microsoft.Extensions.DependencyInjection;

namespace GoogleFormsClient.Extensions;

/// <summary>
/// Extensions to add google forms client.
/// </summary>
public static class ServiceCollectionExtensions
{
    private const string BaseUrl = "https://docs.google.com";

    /// <summary>
    /// Add google form client. After that inject <see cref="IGoogleFormsClient"/> in your services
    /// or create <see cref="GoogleFormsClient"/> directly.
    /// </summary>
    /// <param name="services">Your services.</param>
    /// <returns></returns>
    public static IServiceCollection AddGoogleFormsClient(this IServiceCollection services)
    {
        services.AddHttpClient<IGoogleFormsClient, GoogleFormsClient>()
            .ConfigureHttpClient(client => client.BaseAddress = new Uri(BaseUrl));

        services.AddSingleton<IGoogleFormParser, GoogleFormParser>();

        return services;
    }
}