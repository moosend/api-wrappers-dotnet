using System;

namespace Moosend.Api.Common.Models
{
    public class CampaignParams
    {
        /// <summary>
        ///     The campaign name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        ///     The subject of the emails for the new campaign
        /// </summary>
        public string Subject { get; set; }

        /// <summary>
        ///     The sender of the campaign
        /// </summary>
        public string SenderEmail { get; set; }

        /// <summary>
        ///     The email address to which recipients replies will arrive. It must be one of your sender accounts. If not
        ///     specified, the sender's email will be assumed.
        /// </summary>
        public string ReplyToEmail { get; set; }

        /// <summary>
        ///     The email address to which a confirmation message will be sent when the campaign has been successfully sent. This
        ///     can be any valid email address. It does not have to be one of your sender signatures. If not specified, the
        ///     sender's email will be assumed.
        /// </summary>
        public string ConfirmationToEmail { get; set; }

        /// <summary>
        ///     A url to retrieve the html content for the campaign. We'll automatically move all CSS inline.
        /// </summary>
        public string WebLocation { get; set; }

        /// <summary>
        ///     The ID of a mailing list in your account to which the campaign will be sent to.
        /// </summary>
        public Guid MailingListId { get; set; }

        /// <summary>
        ///     The ID of a segment in the specified mailing list to filter the recipients with. If not specified, the campaign
        ///     will be sent to all active members of the mailing list.
        /// </summary>
        public int SegmentId { get; set; }

        /// <summary>
        ///     Defines the way to split an AB campaign. If omitted, a regular campaign will be sent.
        /// </summary>
        public AbCampaignType? AbCampaignType { get; set; }

        /// <summary>
        ///     If you wish to send an AB split campaign with two different versions of the subject line (ABCampaignType=Subject),
        ///     you must specify the second subject using this parameter. If specified in any other campaign type, it will be
        ///     ignored.
        /// </summary>
        public string SubjectB { get; set; }

        /// <summary>
        ///     If you wish to send an AB split campaign with two different versions of the html content (ABCampaignType=Content),
        ///     you must specify where the second html content will be retrieved from using this parameter. If specified in any
        ///     other campaign type, it will be ignored.
        /// </summary>
        public string WebLocationB { get; set; }

        /// <summary>
        ///     If you wish to send an AB split campaign with two different versions of the sender (ABCampaignType=Sender), you
        ///     must specify the second sender email address using this parameter. This must be one of your sender signatures
        ///     defined in your account. If specified in any other campaign type, it will be ignored.
        /// </summary>
        public string SenderEmailB { get; set; }

        /// <summary>
        ///     If you choose to send an AB campaign type, you must set this parameter to specify how long the test will run,
        ///     before determining which will be the winning version to be sent to the rest of the recipients. This should be an
        ///     integer value between 1 and 24. If specified in a regural campaign, it will be ingored.
        /// </summary>
        public int HoursToTest { get; set; }

        /// <summary>
        ///     If you choose to send an AB campaign type, you must set this parameter to specify a portion of the target
        ///     recipients that will receive the test versions. For example, if you specify 10, then 10% of your recipients will
        ///     recieve the A version and another 10% will receive the B version. The specified value should be an integer between
        ///     5 and 40. If specified in a regural campaign, it will be ignored.
        /// </summary>
        public int ListPercentage { get; set; }

        /// <summary>
        ///     Specifies the method to determine the winning version of an AB campaign after the the test has ended.
        ///     If not set, OpenRate will be assumed. If specified in a regural campaign, it will be ignored.
        /// </summary>
        public AbWinnerSelectionType AbWinnerSelectionType { get; set; }
    }
}
