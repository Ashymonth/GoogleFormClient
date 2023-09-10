using GoogleFormsClient.Exceptions;

namespace GoogleFormsClient.Contracts;

/// <summary>
/// Google form question information.
/// </summary>
public record GoogleFormQuestion
{
    private const string EmptyQuestionNameErrorMessage = "Question name can't be empty";

    /// <summary>
    /// Create a new instance of the <see cref="GoogleFormQuestion"/>
    /// </summary>
    public GoogleFormQuestion()
    {
    }

    internal GoogleFormQuestion(string? name, QuestionType questionType)
    {
        if (string.IsNullOrWhiteSpace(name) && questionType != QuestionType.GridChoiceField)
        {
            throw new UnableParseGoogleFormException(EmptyQuestionNameErrorMessage);
        }

        Name = name;
        Type = questionType;
    }

    /// <summary>
    /// Id of the question.
    /// </summary>
    public int EntryId { get; set; }

    /// <summary>
    /// Question name. If question is GridChoiceQuestion - then null.
    /// </summary>
    public string? Name { get; set; }

    /// <summary>
    /// Question type.
    /// </summary>
    public QuestionType Type { get; set; }

    /// <summary>
    /// Is the question required.
    /// </summary>
    public bool IsRequired { get; set; }

    /// <summary>
    /// Answers of the question.
    /// </summary>
    public List<GoogleFormAnswer> Answers { get; set; } = new();

    /// <summary>
    /// Child answers if the question if question is GridChoiceQuestion.
    /// </summary>
    public List<GoogleFormQuestion> ChildQuestions { get; set; } = new List<GoogleFormQuestion>();
}