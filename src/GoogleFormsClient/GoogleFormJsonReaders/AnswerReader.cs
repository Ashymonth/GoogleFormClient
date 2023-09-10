using System.Text.Json;
using GoogleFormsClient.Contracts;
using GoogleFormsClient.Exceptions;

namespace GoogleFormsClient.GoogleFormJsonReaders;

/// <summary>
/// Can read answers.
///
/// <example>Example answer structure:
///   ["Хорошо", null, null, null, 0],
///   ["Плохо", null, null, null, 0],
///   ["Ужасно", null, null, null, 0],
///   ["Вариант 4", null, null, null, 0],
///   ["", null, null, null, 1]</example>
/// </summary>
internal readonly ref struct AnswerReader
{
    private const int UserAnswerNumber = 1;
    private const int FormAnswerNumber = 0;
    
    /// <summary>
    /// Read answer from json
    /// </summary>
    /// <param name="reader"><see cref="Utf8JsonReader"/></param>
    /// <returns></returns>
    /// <exception cref="ArgumentOutOfRangeException"></exception>
    /// <exception cref="UnableParseGoogleFormException"></exception>
    public static GoogleFormAnswer ReadAnswer(ref Utf8JsonReader reader)
    {
        var answer = new GoogleFormAnswer
        {
            AnswerName = reader.GetString()
        };

        while (reader.Read())
        {
            if (reader.TokenType == JsonTokenType.Number)
            {
                // 1 - indicate that is user input answer
                // 0 - indicate that is form answer
                answer.AnswerType = reader.GetByte() switch
                {
                    UserAnswerNumber => AnswerType.UserAnswer,
                    FormAnswerNumber => AnswerType.FormAnswer,
                    _ => throw new ArgumentOutOfRangeException()
                };
            }

            return answer;
        }

        throw new UnableParseGoogleFormException("");
    }
}