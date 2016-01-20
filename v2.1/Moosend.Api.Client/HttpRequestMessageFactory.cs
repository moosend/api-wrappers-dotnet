using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using Moosend.Api.Common;

namespace Moosend.Api.Client
{
    public class HttpRequestMessageFactory
    {
        public static Uri Endpoint;
        public static string ApiKey;

        public static HttpRequestMessage Create(HttpMethod method, string path, object queryParams = null)
        {
            if (method == null) throw new ArgumentNullException("method");
            if (Endpoint == null) throw new ArgumentNullException("Endpoint");
            if (path == null) throw new ArgumentNullException("path");
            if (ApiKey == null) throw new ArgumentNullException("ApiKey");

            var sb = new StringBuilder(string.Format("{0}{1}?apiKey={2}",
                Endpoint,
                path,
                ApiKey));

            if (queryParams != null)
            {
                sb.Append("&" + queryParams.ToQueryString());
            }

            var uri = new Uri(Endpoint, sb.ToString());

            return new HttpRequestMessage(method, uri);
        }
    }
}