namespace GoogleFormsClient.Exceptions;

/// <summary>
///  The UnableParserGoogleFormException is thrown when
///  an error occurred during parsing json with google form info
/// </summary>
public class UnableParseGoogleFormException : GoogleFormClientException
{
    internal UnableParseGoogleFormException(string message) : base(message)
    {
        
    }
}