namespace GoogleFormClient.Contracts;

/// <summary>
/// Defines a question id/answer pair that will be send.
/// </summary>
public readonly struct GoogleFormEntry
{
    /// <summary>
    /// Create a new instance of the <see cref="GoogleFormEntry"/>
    /// </summary>
    /// <param name="questionId">Identifier of the question.</param>
    /// <param name="answerName">Answer name.</param>
    /// <exception cref="ArgumentNullException"></exception>
    public GoogleFormEntry(string questionId, string answerName)
    {
 
        if (string.IsNullOrWhiteSpace(questionId))
        {
            throw new ArgumentNullException(nameof(questionId));
        }
        
        if (string.IsNullOrWhiteSpace(answerName))
        {
            throw new ArgumentNullException(nameof(answerName));
        }
        
        QuestionId = questionId;
        AnswerName = answerName;
    }
  
    /// <summary>
    /// Question identifier.
    /// </summary>
    public string QuestionId { get; } = null!;

    /// <summary>
    /// Answer name.
    /// </summary>
    public string AnswerName { get; } = null!;

    internal string QuestionEntryId => $"entry.{QuestionId}";
}