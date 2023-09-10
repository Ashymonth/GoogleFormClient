namespace GoogleFormClient.Exceptions;

/// <summary>
/// Represents application specific errors that occur during application execution
/// </summary>
public class GoogleFormClientException : Exception
{
    /// <summary>
    /// Create a new instance of the <see cref="GoogleFormClientException"/>
    /// </summary>
    /// <param name="message">Exception message.</param>
    protected GoogleFormClientException(string message) : base(message)
    {
        
    }
}