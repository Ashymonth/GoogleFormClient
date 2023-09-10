using System.Net;
using System.Text.Json;
using GoogleFormsClient.Contracts;
using GoogleFormsClient.Parsers;
using Moq;
using Moq.Contrib.HttpClient;

namespace GoogleFormsClient.Tests;

public class GoogleFormClientTests
{
    [Theory]
    [ClassData(typeof(GoogleFormClientTestsData))]
    public async Task GetGoogleFormAsyncTest_Should_Return_Google_Form(string googleFormPage, string googleFormAsJson)
    {
        const string requestGoogleFormId = "adfadfasdfaf";

        var expected = JsonSerializer.Deserialize<GoogleForm>(googleFormAsJson,
            new JsonSerializerOptions {Converters = {new GoogleFormConverter()}});

        var moq = new Mock<HttpMessageHandler>();

        moq.SetupRequest(HttpMethod.Get, $"https://docs.google.com/forms/d/e/{requestGoogleFormId}/viewform")
            .ReturnsResponse(HttpStatusCode.OK, message => message.Content = new StringContent(googleFormPage));

        var httpClient = moq.CreateClient();
        httpClient.BaseAddress = new Uri("https://docs.google.com");

        var googleFormClient = new GoogleFormsClient(httpClient, new GoogleFormParser());

        var actual = await googleFormClient.GetGoogleFormAsync(requestGoogleFormId);

        Assert.Equivalent(expected, actual);
    }

    private class GoogleFormClientTestsData : TheoryData<string, string>
    {
        public GoogleFormClientTestsData()
        {
            Add(File.ReadAllText("TestData\\GoogleFormPage.txt"),
                File.ReadAllText("TestData\\GoogleFormPageResult.txt"));

            Add(File.ReadAllText("TestData\\GoogleFormPageWithCheckboxes.txt"),
                File.ReadAllText("TestData\\GoogleFormPageWithCheckboxesResult.txt"));
        }
    }
}