namespace GoogleFormsClient.Contracts;

/// <summary>
/// Possible answers
/// </summary>
public enum AnswerType
{
    /// <summary>
    /// The answer to the question is in the form.
    /// </summary>
    FormAnswer,
    /// <summary>
    /// The user must answer the question himself.
    /// </summary>
    UserAnswer
}