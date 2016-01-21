using System.ComponentModel;

namespace Moosend.Api.Common
{
    public enum AbWinner
    {
        A = 0,
        B = 1
    }

    public enum MailingListStatus
    {
        Created = 0,
        Imported = 1,
        Importing = 2,
        Deleted = 3
    }

    public enum MailStatus
    {
        [Description("Sent")] Sent = 0,

        [Description("Opened")] Opened = 1,

        [Description("Bounced")] Bounced = 2,

        [Description("Not opened")] NotOpened = 5,

        [Description("Link clicked")] LinkClicked = 3,

        [Description("Awaiting delivery")] ReadyToSend = 4,

        [Description("Error")] Error = 6,

        [Description("Forwarded")] Forward = 7,

        [Description("Unsubscribed")] Unsubscribed = 8,

        Complained = 9
    }

    public enum SegmentMatchType
    {
        /// <summary>
        ///     Used in a segment to return the members in a mailing list that match all the given criteria
        /// </summary>
        All = 0,

        /// <summary>
        ///     Used in a segment to return the members in a mailing list that match any of the given criteria
        /// </summary>
        Any = 1
    }

    public enum SegmentFieldType
    {
        Text = 0,
        Integer = 1,
        DateTime = 2,
        List = 3,
        Decimal = 4,
        boolean = 5,
        Hash = 98,
        CustomField = 99
    }

    public enum SegmentCriteriaField
    {
        /// <summary>
        ///     Filters members by the date they where added in the mailing list
        /// </summary>
        [Description("Date Added")] DateAdded = 1,

        /// <summary>
        ///     Filters members by the recipient name
        /// </summary>
        [Description("Recipient Name")] RecipientName = 2,

        /// <summary>
        ///     Filters members by their email address
        /// </summary>
        [Description("Recipient Email")] RecipientEmail = 3,

        /// <summary>
        ///     Filters members according to how many campaigns they have opened (within the past 60 days maximum)
        /// </summary>
        [Description("Campaigns Opened")] CampaignsOpened = 4,

        /// <summary>
        ///     Filters members according to how many links they have clicked from previous campaigns sent to them (within the past
        ///     60 days maximum)
        /// </summary>
        [Description("Links Clicked")] LinksClicked = 5,

        /// <summary>
        ///     Filters members according to which campaigns they have opened, based on their names
        /// </summary>
        [Description("Campaign Name")] CampaignName = 6,

        /// <summary>
        ///     Filters members according to which links they have clicked, based on their urls
        /// </summary>
        [Description("Link URL")] LinkUrl = 7,

        /// <summary>
        ///     Filters members according to the type of the devices they use
        /// </summary>
        [Description("Mobile vs Desktop")] Platform = 8,

        /// <summary>
        ///     Filters members according to the operating systems they use
        /// </summary>
        [Description("Operating System")] OperatingSystems = 9,

        /// <summary>
        ///     Filters members according to the email clients they use
        /// </summary>
        [Description("Email Client")] EmailClient = 10,

        /// <summary>
        ///     Filters members according to the desktop web browsers they use
        /// </summary>
        [Description("Web Browser")] WebBrowser = 11,

        /// <summary>
        ///     Filters members according to the mobile browsers they use
        /// </summary>
        [Description("Mobile Browser")] MobileBrowser = 12,

        /// <summary>
        ///     Filters members according to the custom field specified by the CustomFieldID property
        /// </summary>
        [Description("Custom Field")] CustomField = 99
    }

    public enum SegmentCriteriaComparer
    {
        /// <summary>
        ///     Used to find members where the field is exactly equal to the search term
        /// </summary>
        [Description("is")] Is = 0,

        /// <summary>
        ///     Used to find members where the field is other than the search term
        /// </summary>
        [Description("is not")] IsNot = 1,

        /// <summary>
        ///     Used to find members where the field contains the search term
        /// </summary>
        [Description("contains")] Contains = 2,

        /// <summary>
        ///     Used to find members where the field does not contain the search term
        /// </summary>
        [Description("does not contain")] DoesNotContain = 3,

        /// <summary>
        ///     Used to find members where the field starts with the search term
        /// </summary>
        [Description("starts with")] StartsWith = 4,

        /// <summary>
        ///     Used to find members where the field does not start with the search term
        /// </summary>
        [Description("does not start with")] DoesNotStartWith = 5,

        /// <summary>
        ///     Used to find members where the field ends with the search term
        /// </summary>
        [Description("ends with")] EndsWith = 6,

        /// <summary>
        ///     Used to find members where the field does not end with the search term
        /// </summary>
        [Description("does not end with")] DoesNotEndWith = 7,

        /// <summary>
        ///     Used to find members where the field is greater than the search term
        /// </summary>
        [Description("is greater than")] IsGreaterThan = 8,

        /// <summary>
        ///     Used to find members where the field is greater than or equal to the search term
        /// </summary>
        [Description("is greater than or equal to")] IsGreaterThanOrEqualTo = 9,

        /// <summary>
        ///     Used to find members where the field is less than the search term
        /// </summary>
        [Description("is less than")] IsLessThan = 10,

        /// <summary>
        ///     Used to find members where the field is less than or equal to the search term
        /// </summary>
        [Description("is less than or equal to")] IsLessThanOrEqualTo = 11,

        /// <summary>
        ///     Used to find members where the specified date field is before the specified date value
        /// </summary>
        [Description("is before")] IsBefore = 12,

        /// <summary>
        ///     Used to find members where the specified date field is after the specified date value
        /// </summary>
        [Description("is after")] IsAfter = 13,

        /// <summary>
        ///     Used to find members where the field contains no value
        /// </summary>
        [Description("is empty")] IsEmpty = 14,

        /// <summary>
        ///     Used to find members excluding those where the field contains no value
        /// </summary>
        [Description("is not empty")] IsNotEmpty = 15,

        /// <summary>
        ///     Used to find members where the condition defined by the field is true
        /// </summary>
        [Description("is true")] IsTrue = 16,

        /// <summary>
        ///     Used to find members where the condition defined by the field is false
        /// </summary>
        [Description("is false")] IsFalse = 17
    }

    public enum CustomFieldType
    {
        Text = 0,
        Number = 1,
        DateTime = 2,
        SingleSelectDropdown = 3,
        CheckBox = 5
    }

    public enum SubscribeType
    {
        /// <summary>
        ///     Represents an active member
        /// </summary>
        [Description("Active")] Subscribed = 1,

        /// <summary>
        ///     Represents an unsubscribed member
        /// </summary>
        Unsubscribed = 2,

        /// <summary>
        ///     Represents a member that has bounced on a previously sent campaign and is probably not valid
        /// </summary>
        Bounced = 3,

        /// <summary>
        ///     Represents a removed member pending deletion from our database
        /// </summary>
        Removed = 4
    }

    public enum FormatType
    {
        Html,
        Template,
        PlainText
    }

    public enum CampaignStatus
    {
        Draft = 0,
        ReadyToSend = 1,
        Sent = 3,
        SmtpReadyToSend = 5,
        NotEnoughCredits = 4,
        Sending = 6,
        SelectingWinner = 11,
        Archived = 12
    }

    public enum AbCampaignType
    {
        /// <summary>
        ///     Test two different versions of the sender name and send the final campaign to the one performing better
        /// </summary>
        Sender,

        /// <summary>
        ///     Test two different versions of the campaign content and send the final campaign to the one performing better
        /// </summary>
        Content,

        /// <summary>
        ///     Test two different versions of the subject line and send the final campaign to the one performing better
        /// </summary>
        SubjectLine
    }

    public enum AbWinnerSelectionType
    {
        /// <summary>
        ///     Used to determine the winner of an AB campaign according to which version achieved more opens
        /// </summary>
        OpenRate,

        /// <summary>
        ///     Used to determine the winner of an AB campaign according to which version achieved more unique link clicks
        /// </summary>
        TotalUniqueClicks
    }

    public enum AbVersion
    {
        A,
        B
    }
}
