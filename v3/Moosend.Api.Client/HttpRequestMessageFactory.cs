using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace Moosend.Api.Client
{
    public class HttpRequestMessageFactory
    {
        public HttpRequestMessage Create(HttpMethod method, Uri endpoint, string path, string apiKey, IDictionary<string, string> queryParams = null)
        {
            if (method == null) throw new ArgumentNullException("method");
            if (endpoint == null) throw new ArgumentNullException("endpoint");
            if (path == null) throw new ArgumentNullException("path");
            if (apiKey == null) throw new ArgumentNullException("apiKey");

            var sb = new StringBuilder(string.Format("{0}{1}?apiKey={2}",
                endpoint,
                path,
                apiKey));

            if (queryParams != null)
            {
                foreach (var queryParam in queryParams)
                {
                    sb.Append(string.Format("&{0}={1}", queryParam.Key, queryParam.Value));
                }
            }

            var uri = new Uri(endpoint, sb.ToString());

            return new HttpRequestMessage(method, uri);
        }
    }
}