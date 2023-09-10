using System.Text.Json;
using GoogleFormClient.Contracts;
using GoogleFormClient.Extensions;

namespace GoogleFormClient.GoogleFormJsonReaders;

/// <summary>
/// Can read this format of data
/// [
///     1531452529, - question id
///     [["Example answer"], ["Example answer 2"], ["Example answer 3"]], - answer names
///     0, - is question required
///     ["Example question name"], - question name
///     null,
///     null,
///     null,
///     null,
///     null,
///     null,
///     null,
///     [0]
/// ],
/// </summary>
internal readonly ref struct GridChooseQuestionReader
{
    private const byte RequiredAnswerMarker = 1; // 1 - required, 0 - not required

    /// <summary>
    /// Read question from grid choose question
    /// </summary>
    /// <param name="reader"><see cref="Utf8JsonReader"/></param>
    /// <returns></returns>
    public static GoogleFormQuestion ReadQuestion(ref Utf8JsonReader reader)
    {
        var question = new GoogleFormQuestion
        {
            Type = QuestionType.ShortAnswerField,
            EntryId = reader.GetInt32()
        };

        reader.Read(); // go to the next element

        // example answers format:
        //  [["Answer 1"], ["Answer 2"], ["Answer 3"]],
        while (reader.TokenType != JsonTokenType.Number)
        {
            reader.Read();

            if (reader.TokenType != JsonTokenType.String)
            {
                continue;
            }
            
            question.Answers.Add(AnswerReader.ReadAnswer(ref reader));
        }

        question.IsRequired = reader.GetByte() == RequiredAnswerMarker; 

        reader.Read(); // go to next
        reader.Read(); // skip start array

        question.Name = reader.GetStringOrError("Question name can't be empty");

        return question;
    }
}