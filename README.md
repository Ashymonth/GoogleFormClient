# GoogleFormClient

This client library allows you to get the structure of a Google form with answer IDs and also send these answers to the google forms.

First of all, you need to add this client to your collection of services.

```
services.AddGoogleFormsClient();
```

Then you need to inject client into your service


```
public class Example
{    
    public Example(IGoogleFormClient googleFormClient)
    {
        
    }
}
```

To get the google form structure, call the method ```GetGoogleFormAsync(string googleFormId)``` and pass it the google form id

```
public class Example
{
    private readonly IGoogleFormClient _googleFormClient;

    public Example(IGoogleFormClient googleFormClient)
    {
        _googleFormClient = googleFormClient;
    }

    public async Task ExampleMethod(string googleFormId)
    {
        await _googleFormClient.GetGoogleFormAsync(googleFormId);
    }
}
```

if the page is not found you will get ```HttpRequestException```
if the page has errors (empty question name, empty answer name, etc) then you will get ```UnableParseGoogleFormException```

To send answers you need to call the method ```SendGoogleFormAsync(googleFormId, googleFormEntries)``` and pass it the google form id and answers for question.

```
public class Example
{
    private readonly IGoogleFormClient _googleFormClient;

    public Example(IGoogleFormClient googleFormClient)
    {
        _googleFormClient = googleFormClient;
    }

    public async Task ExampleMethod(string googleFormId, IEnumerable<GoogleFormEntry>  entries)
    {
        await _googleFormClient.SendGoogleFormAsync(googleFormId, entries);
    }
}
```
