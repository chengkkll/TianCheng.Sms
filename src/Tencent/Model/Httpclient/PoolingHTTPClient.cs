using System;
using System.Text;
using System.Net.Http;
using System.Net.Http.Headers;


namespace TianCheng.Sms.Tencent.Model
{
    public class PoolingHTTPClient : IHTTPClient
    {
        private static HttpClient client = new HttpClient();

        public HTTPResponse fetch(HTTPRequest request)
        {
            UriBuilder uriBuilder = new UriBuilder(request.url);
            StringBuilder query = new StringBuilder();
            foreach (var parameter in request.parameters)
            {
                query.Append(parameter.Key);
                query.Append("=");
                query.Append(parameter.Value);
                query.Append("&");
            }
            query.Length--;  // Remove the last '&' character
            uriBuilder.Query = query.ToString();

            HttpRequestMessage msg = new HttpRequestMessage();
            msg.RequestUri = uriBuilder.Uri;
            request.url = uriBuilder.Uri.ToString();
            msg.Method = new HttpMethod(request.method.ToString());
            msg.Content = new StringContent(request.body, request.bodyEncoding);
            foreach (var header in request.headers)
            {
                if (header.Key == "Content-Type")
                {
                    msg.Content.Headers.ContentType = new MediaTypeHeaderValue(header.Value);
                }
                else
                {
                    msg.Headers.TryAddWithoutValidation(header.Key, header.Value);
                }
            }
            // Fetch http response
            try
            {
                // Sync send request
                HttpResponseMessage response = client.SendAsync(msg).Result;
                // Sync read response body
                string responseBody = response.Content.ReadAsStringAsync().Result;

                HTTPResponse res = new HTTPResponse()
                    .setRequest(request)
                    .setStatusCode((int)response.StatusCode)
                    .setReason(response.ReasonPhrase)
                    .setBody(responseBody);

                foreach (var header in response.Headers)
                {
                    foreach (var value in header.Value)
                    {
                        res.addHeader(header.Key, value);
                    }
                }

                return res;
            }
            catch (HttpRequestException)
            {
                // not handle, re-throw
                throw;
            }
        }

        public void close()
        {
            // Do nothing
        }
    }
}