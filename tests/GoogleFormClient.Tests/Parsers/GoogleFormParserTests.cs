using GoogleFormClient.Parsers;

namespace GoogleFormClient.Tests.Parsers;

public class GoogleFormParserTests
{
    [Theory]
    [ClassData(typeof(GoogleFormParserTheoryData))]
    public void GetGoogleFormJsonAsyncTest_Should_Return_Valid_Json(string googleFormPage, string expected)
    {
        var parser = new GoogleFormParser();

        string actual = parser.GetGoogleFormJsonAsync(googleFormPage);

        Assert.Equal(expected, actual);
    }

    private class GoogleFormParserTheoryData : TheoryData<string, string>
    {
        public GoogleFormParserTheoryData()
        {
            Add(File.ReadAllText("TestData\\GoogleFormPage.txt"),
                File.ReadAllText("TestData\\GoogleFormPageResult.txt"));
            
            Add(File.ReadAllText("TestData\\GoogleFormPageWithCheckboxes.txt"),
                File.ReadAllText("TestData\\GoogleFormPageWithCheckboxesResult.txt"));
        }
    }
}