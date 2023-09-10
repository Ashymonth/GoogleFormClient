# GoogleFormsClient

This client library allows you to get the structure of a Google form with answer IDs and also send these answers to the google forms.

First of all, you need to add this client to your collection of services.

```
services.AddGoogleFormsClient();
```

Then you need to inject client into your service


```
public class Example
{    
    public Example(IGoogleFormsClient googleFormsClient)
    {
        
    }
}
```

To get the google form structure, call the method ```GetGoogleFormsAsync(string googleFormsId)``` and pass it the google form id

```
public class Example
{
    private readonly IGoogleFormsClient _GoogleFormsClient;

    public Example(IGoogleFormsClient googleFormsClient)
    {
        _googleFormsClient = googleFormsClient;
    }

    public async Task ExampleMethod(string GoogleFormsId)
    {
        await _googleFormsClient.GetGoogleFormsAsync(GoogleFormsId);
    }
}
```

if the page is not found you will get ```HttpRequestException```
<br>
if the page has errors (empty question name, empty answer name, etc) you will get ```UnableParseGoogleFormsException```

To send answers you need to call the method ```SendGoogleFormsAsync(googleFormsId, googleFormsEntries)``` and pass it the google form id and answers for question.

```
public class Example
{
    private readonly IGoogleFormsClient _GoogleFormsClient;

    public Example(IGoogleFormsClient googleFormsClient)
    {
        _googleFormsClient = googleFormsClient;
    }

    public async Task ExampleMethod(string GoogleFormsId, IEnumerable<GoogleFormsEntry>  entries)
    {
        await _googleFormsClient.SendGoogleFormAsync(GoogleFormsId, entries);
    }
}
```
