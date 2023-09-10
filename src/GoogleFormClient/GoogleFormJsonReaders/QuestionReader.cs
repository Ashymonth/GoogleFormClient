using System.Text.Json;
using GoogleFormClient.Contracts;

namespace GoogleFormClient.GoogleFormJsonReaders;

/// <summary>
/// Can read questions except GridChoiceQuestion.
/// </summary>
internal readonly ref struct QuestionReader
{
    /// <summary>
    /// Read question.
    /// </summary>
    /// <param name="reader"></param>
    /// <returns></returns>
    public static GoogleFormQuestion ReadQuestion(ref Utf8JsonReader reader)
    {
        reader.Read(); // skip question id

        string? questionName = reader.GetString();

        reader.Read(); // skip null
        reader.Read(); // read question type

        int questionType = reader.GetInt32();

        var question = new GoogleFormQuestion(questionName, (QuestionType) questionType);

        return question;
    }
}