using System.Text.Json;
using GoogleFormsClient.Exceptions;

namespace GoogleFormsClient.Extensions;

internal static class Utf8JsonReaderExtensions
{
    private const string EmptyAnswerNameErrorMessage = "Answer name can't be empty";
    private const string EmptyGoogleFormNameErrorMessage = "Google form name can't be empty";

    public static string GetAnswerName(this Utf8JsonReader reader) =>
        reader.GetStringOrError(EmptyAnswerNameErrorMessage);

    public static string GetGoogleFormName(this Utf8JsonReader reader) =>
        reader.GetStringOrError(EmptyGoogleFormNameErrorMessage);

    public static string GetStringOrError(this Utf8JsonReader reader, string errorMessage)
    {
        string? result = reader.GetString();

        if (string.IsNullOrWhiteSpace(result))
        {
            throw new UnableParseGoogleFormException(errorMessage);
        }

        return result;
    }
}