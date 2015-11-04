using System;
using System.Collections.Generic;
using System.Text;
using Moosend.API.Client.Wrappers;
using Moosend.API.Client;
using System.Net;
using System.IO;
using Newtonsoft.Json;

namespace Moosend.API.Client
{
    public class ApiManager : IApiManager
    {
        private static readonly String END_POINT = "http://api.moosend.com";

        public ApiManager()
        {
        }

        public ApiManager(string apiKey)
        {
            this.ApiKey = apiKey;
        }

        public ApiManager(ICampaignsWrapper campaignsWrapper, IMailingListsWrapper mailingListsWrapper, ISubscribersWrapper subscriberWrapper, ISegmentsWrapper segmentsWrapper)
        {
            _Campaigns = campaignsWrapper;
            _MailingLists = mailingListsWrapper;
            _Subscribers = subscriberWrapper;
            _Segments = segmentsWrapper;
        }

        public String ApiKey
        {
            get;
            set;
        }

        private ICampaignsWrapper _Campaigns;

        public ICampaignsWrapper Campaigns
        {
            get
            {
                if (_Campaigns == null)
                {
                    _Campaigns = new CampaignsWrapper(this);
                }
                return _Campaigns;
            }
            private set
            {
                _Campaigns = value;
            }
        }

        private ISubscribersWrapper _Subscribers;

        public ISubscribersWrapper Subscribers
        {
            get
            {
                if (_Subscribers == null)
                {
                    _Subscribers = new SubscribersWrapper(this);
                }
                return _Subscribers;
            }
            private set
            {
                _Subscribers = value;
            }
        }

        private IMailingListsWrapper _MailingLists;

        public IMailingListsWrapper MailingLists
        {
            get
            {
                if (_MailingLists == null)
                {
                    _MailingLists = new MailingListsWrapper(this);
                }
                return _MailingLists;
            }
            private set
            {
                _MailingLists = value;
            }
        }

        private ISegmentsWrapper _Segments;

        public ISegmentsWrapper Segments
        {
            get
            {
                if (_Segments == null)
                {
                    _Segments = new SegmentsWrapper(this);
                }
                return _Segments;
            }
            private set
            {
                _Segments = value;
            }
        }

        public T MakeRequest<T>(HttpMethod method, String path)
        {
            return MakeRequest<T>(method, path, new { });
        }

        public void MakeRequest(HttpMethod method, String path)
        {
            MakeRequest<object>(method, path);
        }

        public void MakeRequest(HttpMethod method, String path, Object parameters)
        {
            MakeRequest<object>(method, path, parameters);
        }

        public T MakeRequest<T>(HttpMethod method, String path, Object parameters)
        {
            string query;

            if (method == HttpMethod.GET)
            {
                // serialize parameters to be used in the query string
                query = parameters.ToQueryString();
            }
            else
            {
                // serialize parameters in json format to be used in POST and DELETE request streams
                query = JsonConvert.SerializeObject(parameters);
            }

            // build uri for request
            String parametersWithApiKey = "apiKey=" + ApiKey + "&mode=Data";
            if (!string.IsNullOrEmpty(query) && method == HttpMethod.GET) parametersWithApiKey += "&" + query;
            String uri = END_POINT + path + ".json?" + parametersWithApiKey;

            // initialize web request
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(uri);
            req.Method = method.ToString();
            req.Accept = "application/json";
            req.ContentType = "application/json";
            req.Timeout = 180000; // 3 minutes
            req.AutomaticDecompression = DecompressionMethods.GZip;            
            req.UserAgent = String.Format("moosend-api-{0}-{1}", Environment.Version, Environment.OSVersion);
            req.KeepAlive = false;

            if (method != HttpMethod.GET)
            {
                req.ContentLength = Encoding.UTF8.GetByteCount(query);
                
                // write serialized parameters to the request stream
                using (StreamWriter streamWriter = new StreamWriter(req.GetRequestStream()))
                {
                    streamWriter.Write(query);
                    streamWriter.Close();
                }
            }

            try
            {
                // get response stream and deserialize to expected object
                using (HttpWebResponse response = (HttpWebResponse)req.GetResponse())
                {
                    if (response == null) return default(T);

                    using (Stream responseStream = response.GetResponseStream())
                    using (StreamReader readStream = new StreamReader(responseStream))
                    {
                        string json = readStream.ReadToEnd().Trim();

                        // deserialize as a generic api result and check if result is an error
                        var result = JsonConvert.DeserializeObject<ApiResult<object>>(json);
                        if (result.Code == 0)
                        {
                            // deserialize again to get the expected object
                            return JsonConvert.DeserializeObject<ApiResult<T>>(json).Context;
                        }
                        else
                        {
                            // throw exception if deserialized object represents an error
                            throw new ApiException(result.Error);
                        }
                    }
                }
            }
            catch (WebException we)
            {
                throw we;
            }

        }

    }
}
