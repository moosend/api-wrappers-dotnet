using Moosend.API.Client.Wrappers;

namespace Moosend.API.Client
{
    public interface IApiManager
    {
        string ApiKey { get; set; }
        CampaignsWrapper Campaigns { get; }
        MailingListsWrapper MailingLists { get; }
        SegmentsWrapper Segments { get; }
        SubscribersWrapper Subscribers { get; }

        void MakeRequest(HttpMethod method, string path);
        void MakeRequest(HttpMethod method, string path, object parameters);
        T MakeRequest<T>(HttpMethod method, string path);
        T MakeRequest<T>(HttpMethod method, string path, object parameters);
    }
}