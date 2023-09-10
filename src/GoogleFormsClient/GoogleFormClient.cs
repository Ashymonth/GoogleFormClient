using System.Text.Json;
using GoogleFormsClient.Contracts;
using GoogleFormsClient.Exceptions;
using GoogleFormsClient.Parsers;

namespace GoogleFormsClient;

/// <summary>
/// Client to communicate with google forms.
/// </summary>
public interface IGoogleFormsClient
{
    /// <summary>
    /// Get google form from google form page.
    /// </summary>
    /// <param name="googleFormId">Identifier of the google form.</param>
    /// <param name="ct"><see cref="CancellationToken"/></param>
    /// <returns>Parsed google form structure.</returns>
    /// <exception cref="UnableParseGoogleFormException">If provided google form page was invalid.</exception>
    /// <exception cref="HttpRequestException">The HTTP response is unsuccessful.</exception>
    Task<GoogleForm> GetGoogleFormAsync(string googleFormId, CancellationToken ct = default);

    /// <summary>
    /// Send answers for questions of the google form to google.
    /// </summary>
    /// <param name="googleFormId">Identifier of the google form.</param>
    /// <param name="entries">Answers for questions.</param>
    /// <param name="ct"></param>
    /// <returns></returns>
    Task SendGoogleFormAsync(string googleFormId,
        IEnumerable<GoogleFormEntry> entries,
        CancellationToken ct = default);
}

/// <summary>
/// <see cref="IGoogleFormsClient"/>
/// </summary>
public class GoogleFormsClient : IGoogleFormsClient
{
    private const string GetGoogleFormTemplate = "/forms/d/e/{0}/viewform";
    private const string SendGoogleFormTemplate = "/forms/u/0/d/e/{0}/formResponse";

    private static readonly JsonSerializerOptions Options = new() {Converters = {new GoogleFormConverter()}};

    private readonly HttpClient _httpClient;
    private readonly IGoogleFormParser _googleFormParser;

    /// <summary>
    /// Create a new instance of <see cref="GoogleFormsClient"/>
    /// </summary>
    /// <param name="httpClient"><see cref="HttpClient"/></param>
    /// <param name="googleFormParser"><see cref="ArgumentNullException"/></param>
    /// <exception cref="GoogleFormsClient">http clients is null or googleFormParser is null</exception>
    public GoogleFormsClient(HttpClient httpClient, IGoogleFormParser googleFormParser)
    {
        _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        _googleFormParser = googleFormParser ?? throw new ArgumentNullException(nameof(googleFormParser));
    }

    /// <inheritdoc />
    public async Task<GoogleForm> GetGoogleFormAsync(string googleFormId, CancellationToken ct = default)
    {
        using var response = await _httpClient.GetAsync(string.Format(GetGoogleFormTemplate, googleFormId), ct);
        response.EnsureSuccessStatusCode();

        string page = await response.Content.ReadAsStringAsync(ct);

        string parsedGoogleForm = _googleFormParser.GetGoogleFormJsonAsync(page);

        var googleForm = JsonSerializer.Deserialize<GoogleForm>(parsedGoogleForm, Options);

        return googleForm!;
    }

    /// <inheritdoc />
    public async Task SendGoogleFormAsync(string googleFormId,
        IEnumerable<GoogleFormEntry> entries,
        CancellationToken ct = default)
    {
        var sendData = entries
            .Select(entry => new KeyValuePair<string, string>(entry.QuestionEntryId, entry.AnswerName))
            .ToArray();

        using var request = new HttpRequestMessage(HttpMethod.Post, string.Format(SendGoogleFormTemplate, googleFormId))
            {Content = new FormUrlEncodedContent(sendData)};

        using var response = await _httpClient.SendAsync(request, ct);
        response.EnsureSuccessStatusCode();
    }
}