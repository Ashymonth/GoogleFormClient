using GoogleFormClient.Exceptions;
using HtmlAgilityPack;
using Microsoft.Extensions.Logging;

namespace GoogleFormClient.Parsers;

/// <summary>
/// Parser for google form question and answers structure.
/// </summary>
public interface IGoogleFormParser
{
    /// <summary>
    /// Extract json with question and answers info from google form page.
    /// </summary>
    /// <param name="googleFormUrl">Url to the google form.</param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    string GetGoogleFormJsonAsync(string googleFormUrl);
}

/// <summary>
/// <see cref="IGoogleFormParser"/>
/// </summary>
internal class GoogleFormParser : IGoogleFormParser
{
    private const int TextLengthToClean = 28; // var FB_PUBLIC_LOAD_DATA_ = 
    private const string JsonStartTextMarker = "var FB_PUBLIC_LOAD_DATA_ = ";
    private const string ScriptTag = "script";

    private readonly ILogger<GoogleFormParser>? _logger;

    public GoogleFormParser(ILogger<GoogleFormParser>? logger = null) => _logger = logger;

    public string GetGoogleFormJsonAsync(string googleFormUrl)
    {
        var document = new HtmlDocument();
        document.LoadHtml(googleFormUrl);
        
        if (TryGetJsonFast(document, out string? googleFormJson))
        {
            return googleFormJson!;
        }

        if (TryGetJson(document, out googleFormJson))
        {
            return googleFormJson!;
        }

        throw new UnableParseGoogleFormException("Unable to get google form structure from provided page");
    }

    private bool TryGetJsonFast(HtmlDocument document, out string? googleFormJson)
    {
        string? googleFormJsonScript = document.DocumentNode.Descendants(ScriptTag)
            .SkipLast(1) // skip last script to get our
            .LastOrDefault()
            ?.InnerText;

        if (string.IsNullOrWhiteSpace(googleFormJsonScript))
        {
            googleFormJson = null;
            return false;
        }

        ReadOnlySpan<char> htmlAsSpan;
        try
        {
            htmlAsSpan = googleFormJsonScript.AsSpan();
        }
        catch (Exception e)
        {
            _logger?.LogWarning(e, "Error on converting google form json to span");
            googleFormJson = null;
            return false;
        }

        if (!htmlAsSpan.StartsWith(JsonStartTextMarker))
        {
            googleFormJson = null;
            return false;
        }

        // cleaning up "var FB_PUBLIC_LOAD_DATA_ = " at the beginning 
        // and ";" at the end of the script text  
        
        var jsonSpan = htmlAsSpan.Slice(TextLengthToClean - 1, htmlAsSpan.Length - TextLengthToClean);
        googleFormJson = jsonSpan.ToString();
        return true;
    }

    private static bool TryGetJson(HtmlDocument document, out string? googleFormJson)
    {
        var htmlNodes = document.DocumentNode.SelectNodes("//script").Where(
            x => x.GetAttributeValue("type", "").Equals("text/javascript") &&
                 x.InnerHtml.Contains("FB_PUBLIC_LOAD_DATA_"));

        string? fbPublicLoadDataJsScriptContent = htmlNodes.First().InnerHtml;

        if (!fbPublicLoadDataJsScriptContent.StartsWith(JsonStartTextMarker))
        {
            googleFormJson = null;
            return false;
        }

        // cleaning up "var FB_PUBLIC_LOAD_DATA_ = " at the beginning 
        // and ";" at the end of the script text  
        string fbPublicJsScriptContentCleanedUp = fbPublicLoadDataJsScriptContent
            .Substring(TextLengthToClean - 1, fbPublicLoadDataJsScriptContent.Length - TextLengthToClean)
            .Trim();

        googleFormJson = fbPublicJsScriptContentCleanedUp;
        return true;
    }
}