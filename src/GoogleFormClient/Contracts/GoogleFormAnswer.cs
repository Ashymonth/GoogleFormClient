namespace GoogleFormClient.Contracts;

/// <summary>
/// Answer for the question.
/// </summary>
public class GoogleFormAnswer
{
    /// <summary>
    /// Answer name.
    /// If answer type is <see cref="Contracts.AnswerType.UserAnswer"/> then null.
    /// </summary>
    public string? AnswerName { get; set; }
    
    /// <summary>
    /// Type of the answer.
    /// </summary>
    public AnswerType AnswerType { get; set; }
}