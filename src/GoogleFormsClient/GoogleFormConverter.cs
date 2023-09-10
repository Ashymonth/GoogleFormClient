using System.Text.Json;
using System.Text.Json.Serialization;
using GoogleFormsClient.Contracts;
using GoogleFormsClient.Extensions;
using GoogleFormsClient.GoogleFormJsonReaders;

namespace GoogleFormsClient;

/// <summary>
/// Converting json form google form page to model.
/// </summary>
internal class GoogleFormConverter : JsonConverter<GoogleForm>
{
    private const byte RequiredAnswerMarker = 1; // 1 - required, 0 - not required
    private const string BeforeGoogleFormNameElement = "/forms";

    private const int QuestionInfoDepthInJson = 4;
    private const int QuestionDepthInJson = 6;
    private const int AnswersDepthInJson = 8;

    public override GoogleForm Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var googleForm = new GoogleForm();

        var currentQuestion = new GoogleFormQuestion();

        while (reader.Read())
        {
            switch (reader.TokenType)
            {
                case JsonTokenType.Null:
                case JsonTokenType.Number when reader.GetInt32() == 0:
                    continue;
                case JsonTokenType.String
                    when reader.GetString() == BeforeGoogleFormNameElement: // next element will be form name
                {
                    reader.Read();
                    googleForm.Name = reader.GetGoogleFormName();
                    break;
                }
                case JsonTokenType.EndArray when reader.CurrentDepth == QuestionDepthInJson:
                    reader.Read();
                    if (reader.TokenType != JsonTokenType.Number)
                    {
                        continue;
                    }

                    currentQuestion.IsRequired = reader.GetByte() == RequiredAnswerMarker;
                    continue;
                default:
                    switch (reader.CurrentDepth)
                    {
                        case QuestionInfoDepthInJson when IsNotArrayToken(reader.TokenType):
                            currentQuestion = QuestionReader.ReadQuestion(ref reader);
                            googleForm.Questions.Add(currentQuestion);
                            break;

                        case QuestionDepthInJson when reader.TokenType == JsonTokenType.Number &&
                                                      currentQuestion.Type == QuestionType.GridChoiceField:
                        {
                            currentQuestion.ChildQuestions.Add(GridChooseQuestionReader.ReadQuestion(ref reader));
                            break;
                        }
                        case QuestionDepthInJson when IsNotArrayToken(reader.TokenType):
                            currentQuestion.EntryId = reader.GetInt32();
                            break;
                        case AnswersDepthInJson when reader.TokenType == JsonTokenType.String:
                        {
                            currentQuestion.Answers.Add(AnswerReader.ReadAnswer(ref reader));
                            break;
                        }
                    }

                    break;
            }
        }

        return googleForm;
    }

    public override void Write(Utf8JsonWriter writer, GoogleForm value, JsonSerializerOptions options)
    {
        throw new NotSupportedException();
    }

    private static bool IsNotArrayToken(JsonTokenType jsonTokenType) =>
        jsonTokenType != JsonTokenType.StartArray && jsonTokenType != JsonTokenType.EndArray;
}