Moosend .NET API Wrapper
=======

Moosend .NET API wrapper allows you to connect to the [Moosend](http://www.moosend.com) API and supports the operations listed below.

Mailing Lists

- List available mailing lists

- Retrieve mailing list details

- Create, update or delete mailing lists

- Create, update or delete custom fields

- Get Subscribers in a mailing list

Subsribers

- Retrieve subscriber details

- Add multiple subscribers

- Subscribe, Unsubscribe or Remove subscribers

Campaigns 

- List available campaigns

- Retrieve campaign details

- Create draft campaigns or clone existing campaigns

- Send a campaign test

- Send a draft campaign to the linked mailing list

- Retrieve statistics and performance metrics for a sent campaign



Usage
=======

To use, simply add a reference to Moosend.API.Client.dll in your project and use the methods available to make requests. For more information you can follow the examples below.

Include the following using statements for your convinience

```c#
	using Moosend.API.Client;
	using Moosend.API.Client.Models;
```

Make a declaration like the following in your class to access the API (or even better use DI to inject using your favorite IoC library)

```c#
	private IApiManager MoosendAPI = new ApiManager("YOUR_API_KEY");
```

Examples
=======

Wrapper Examples

Subscribers

- Subscribe a subscriber to your mailing list

```c#
    var mailingListID = Guid.Parse("YOUR_MAILING_LIST_ID");

    var info = new SubscriberParams()
    {
        Name = "John Doe",
        Email = "john.doe@some-domain.com"
    };

    try
    {
        var subscriber = MoosendAPI.Subscribers.Subscribe(mailingListID, info);

        Console.WriteLine("ID: {0}, Name: {1}, Email: {2}", subscriber.ID, subscriber.Name, subscriber.Email);
    }
    catch (ApiException ex)
    {
        Console.WriteLine("ERROR: {0}", ex.Message);
    }
```

- Unsubscribe a subscriber from a mailing list

```c#
    var mailingListID = Guid.Parse("YOUR_MAILING_LIST_ID");
    var campaignID = Guid.Parse("YOUR_CAMPAIGN_ID");

    var subscriberEmail = "john.doe@some-domain.com";

    try
    {
        MoosendAPI.Subscribers.Unsubscribe(mailingListID, campaignID, subscriberEmail);

        Console.WriteLine("Unsubscribe successfule");
    }
    catch (ApiException ex)
    {
        Console.WriteLine("ERROR: {0}", ex.Message);
    }
```

Lists

- Get all mailing lists in your account

```c#
    var page = 1;
    var pageSize = 10;

    try
    {
        var results = MoosendAPI.MailingLists.FindAllActive(page, pageSize);

        results.ForEach(mailingList =>
            Console.WriteLine("ID: {0}, Name: {1}, Count: {2}", mailingList.ID, mailingList.Name, mailingList.ActiveMemberCount)
        );
    }
    catch (ApiException ex)
    {
        Console.WriteLine("ERROR: {0}", ex.Message);
    }
```

- Get all subscribers from a mailing list

```c#
    var mailingListID = Guid.Parse("YOUR_MAILING_LIST_ID");

    try
    {
        var results = MoosendAPI.MailingLists.GetSubscribers(mailingListID, SubscribeType.Subscribed);

        Console.WriteLine("Page {0} of {1}, Max results per page: {2}, Total results: {3}", 
            results.PagingInfo.CurrentPage, results.TotalPageCount, results.PagingInfo.PageSize, results.TotalResults);

        results.ForEach(subscriber =>
            Console.WriteLine("ID: {0}, Name: {1}, Count: {2}", subscriber.ID, subscriber.Name, subscriber.Email)
        );
    }
    catch (ApiException ex)
    {
        Console.WriteLine("ERROR: {0}", ex.Message);
    }
```

- Create a mailing list

```c#
    try
    {
        var mailingListID = MoosendAPI.MailingLists.Create("Some new test list");

        Console.WriteLine("Created new mailing list with ID: {0}", mailingListID);
    }
    catch (ApiException ex)
    {
        Console.WriteLine("ERROR: {0}", ex.Message);
    }
```

- Get a single subscriber from a mailing list

```c#
    var mailingListID = Guid.Parse("YOUR_MAILING_LIST_ID");
    var subscriberEmail = "john.doe@some-domain.com";

    try
    {
        var subscriber = MoosendAPI.Subscribers.GetByEmail(mailingListID, subscriberEmail);

        Console.WriteLine("ID: {0}, Name: {1}, Email: {2}", subscriber.ID, subscriber.Name, subscriber.Email);
    }
    catch (ApiException ex)
    {
        Console.WriteLine("ERROR: {0}", ex.Message);
    }
```


Campaigns

- Create a new draft campaign (Nothing will be sent to any of your recipients)

```c#
    var info = new CampaignParams()
    {
        Name = "Some campaign",
        Subject = "Some subject",
        MailingListID = Guid.Parse("YOUR_MAILING_LIST_ID"),
        SenderEmail = "YOUR_SENDER_SIGNATURE_EMAIL_ADDRESS",
        ConfirmationToEmail = "SOME_CONFIRMATION_EMAIL_ADDRESS",  // optional
        WebLocation = "THE_WEB_LOCATION_OF_YOUR_CAMPAIGN"                
    };

    try
    {
        var campaignID = MoosendAPI.Campaigns.Create(info);

        Console.WriteLine("Created new campaign with ID: {0}", campaignID);
    }
    catch (ApiException ex)
    {
        Console.WriteLine("ERROR: {0}", ex.Message);
    }
```

- Send a set of test emails of a given campaign

```c#
    var campaignID = Guid.Parse("YOUR_CAMPAIGN_ID");
    var emails = new string [] {"someone@example.com", "somebody@other.com"};

    try
    {
        MoosendAPI.Campaigns.SendTest(campaignID, emails);

        Console.WriteLine("Send test completed successfully");
    }
    catch (ApiException ex)
    {
        Console.WriteLine("ERROR: {0}", ex.Message);
    }
```

- Send the given campaign to all active subscribers in the linked mailing list

```c#
    var campaignID = Guid.Parse("YOUR_CAMPAIGN_ID");

    try
    {
        MoosendAPI.Campaigns.Send(campaignID);

        Console.WriteLine("Send campaign completed successfully");
    }
    catch (ApiException ex)
    {
        Console.WriteLine("ERROR: {0}", ex.Message);
    }
```

- Get a summary of statistics for the given campaign (assuming you have sent the campaign already). For more detailed statistics look at the other methods in the Moosend.API.Client.CampaignsWrapper class (e.g. GetStatistics, GetActivityByLocation, GetLinkActivity)

```c#
    var campaignID = Guid.Parse("YOUR_CAMPAIGN_ID");

    try
    {
        var summary = MoosendAPI.Campaigns.GetSummary(campaignID);

        Console.WriteLine("Name: {0}, Total Opens: {1}, Total Clicks: {2}, Total Bounces: {3}", 
            summary.CampaignName, summary.TotalOpens, summary.TotalLinkClicks, summary.TotalBounces);
    }
    catch (ApiException ex)
    {
        Console.WriteLine("ERROR: {0}", ex.Message);
    }
```
