using System.Text.Json;
using GoogleFormClient.Contracts;

namespace GoogleFormClient.Tests;

public class GoogleFormConverterTests
{
    [Theory]
    [ClassData(typeof(GoogleFormConverterTestData))]
    public void ConvertGoogleFormTest_Should_Deserialize_To_Exact_GoogleForm(string googleFormAsJson,
        GoogleForm expectedForm)
    {
        var options = new JsonSerializerOptions();
        options.Converters.Add(new GoogleFormConverter());

        var actual = JsonSerializer.Deserialize<GoogleForm>(googleFormAsJson, options);

        Assert.Equivalent(expectedForm, actual);
    }

    private class GoogleFormConverterTestData : TheoryData<string, GoogleForm>
    {
        public GoogleFormConverterTestData()
        {
            var googleForm = new GoogleForm
            {
                Name = "Гугл форма",
                Questions = new List<GoogleFormQuestion>()
                {
                    new()
                    {
                        Name = "Тестовый вопрос",
                        EntryId = 1849667742,
                        Type = QuestionType.MultipleChoiceField,
                        IsRequired = false,
                        Answers = new List<GoogleFormAnswer>(new[]
                            {new GoogleFormAnswer {AnswerName = "ТЕстовый ответ"}})
                    },
                    new()
                    {
                        Name = null,
                        EntryId = 0,
                        Type = QuestionType.GridChoiceField,
                        Answers = new List<GoogleFormAnswer>(),
                        IsRequired = false,
                        ChildQuestions = new List<GoogleFormQuestion>()
                        {
                            new()
                            {
                                Name = "Ряд 1",
                                EntryId = 1531452529,
                                Type = QuestionType.ShortAnswerField,
                                Answers = new List<GoogleFormAnswer>(new[]
                                {
                                    new GoogleFormAnswer
                                    {
                                        AnswerName = "Вариант 1",
                                        AnswerType = AnswerType.FormAnswer
                                    },
                                    new GoogleFormAnswer
                                    {
                                        AnswerName = "Столбец 2",
                                        AnswerType = AnswerType.FormAnswer
                                    },
                                    new GoogleFormAnswer
                                    {
                                        AnswerName = "Столбец 3",
                                        AnswerType = AnswerType.FormAnswer
                                    }
                                })
                            },
                            new()
                            {
                                Name = "Ряд 2",
                                EntryId = 498736958,
                                Type = QuestionType.ShortAnswerField,
                                Answers = new List<GoogleFormAnswer>(new[]
                                {
                                    new GoogleFormAnswer
                                    {
                                        AnswerName = "Вариант 1",
                                        AnswerType = AnswerType.FormAnswer
                                    },
                                    new GoogleFormAnswer
                                    {
                                        AnswerName = "Столбец 2",
                                        AnswerType = AnswerType.FormAnswer
                                    },
                                    new GoogleFormAnswer
                                    {
                                        AnswerName = "Столбец 3",
                                        AnswerType = AnswerType.FormAnswer
                                    }
                                })
                            },
                            new()
                            {
                                Name = "Ряд 3",
                                EntryId = 1492679950,
                                Type = QuestionType.ShortAnswerField,
                                Answers = new List<GoogleFormAnswer>(new[]
                                {
                                    new GoogleFormAnswer
                                    {
                                        AnswerName = "Вариант 1",
                                        AnswerType = AnswerType.FormAnswer
                                    },
                                    new GoogleFormAnswer
                                    {
                                        AnswerName = "Столбец 2",
                                        AnswerType = AnswerType.FormAnswer
                                    },
                                    new GoogleFormAnswer
                                    {
                                        AnswerName = "Столбец 3",
                                        AnswerType = AnswerType.FormAnswer
                                    }
                                })
                            }
                        }
                    }
                }
            };

            Add(File.ReadAllText("TestData\\GoogleFormPageResult.txt"), googleForm);
        }
    }
}