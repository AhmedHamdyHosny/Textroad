using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace Classes.Utilities
{
    public class MyHttpRequestMessage
    {
        public HttpRequestMessage RequestMessage { get; set; }
        //public StringContent RequestBody { get; set; }
        public HttpContent RequestBody { get; set; }
        public string OAuthToken { get; set; }

        public MyHttpRequestMessage(string url, HttpMethod webMethod)
        {
            RequestMessage = new HttpRequestMessage();
            RequestMessage.Method = webMethod;
            RequestMessage.RequestUri = new Uri(url);
            
        }

        public async Task<T> Execute<T>() where T : class
        {
            SetRequestBody();
            SetOAuthToken();

            using (HttpClient client = new HttpClient())
            {
                client.Timeout = TimeSpan.FromMinutes(60);
                HttpResponseMessage response = client.SendAsync(RequestMessage).Result;
                if (response.IsSuccessStatusCode)
                {
                    var task = await response.Content.ReadAsAsync<T>();
                    return task;
                }
                else if (response.StatusCode == HttpStatusCode.Unauthorized)
                {
                    throw new UnauthorizedAccessException();
                }
            }
            return null;
        }

        public async Task<byte[]> ExcecuteExport()
        {
            SetRequestBody();
            SetOAuthToken();

            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = client.SendAsync(RequestMessage).Result;
                if (response.IsSuccessStatusCode)
                {
                    var task = await response.Content.ReadAsByteArrayAsync();
                    return task;
                }
                else if (response.StatusCode == HttpStatusCode.Unauthorized)
                {
                    throw new UnauthorizedAccessException();
                }
            }
            return null;
        }
        
        public async Task<string> ExcecuteImport(System.Web.HttpPostedFileBase file)
        {
            SetRequestBody();
            SetOAuthToken();

            using (HttpClient client = new HttpClient())
            {
                client.Timeout = TimeSpan.FromMinutes(60);
                HttpResponseMessage response = client.SendAsync(RequestMessage).Result;
                if (response.IsSuccessStatusCode)
                {
                    string result = await response.Content.ReadAsStringAsync();
                    return result;
                }
                else if (response.StatusCode == HttpStatusCode.Unauthorized)
                {
                    throw new UnauthorizedAccessException();
                }
            }

            return null;
        }

        public async Task<string> ExcecuteAsString()
        {
            SetRequestBody();
            SetOAuthToken();

            using (HttpClient client = new HttpClient())
            {
                client.Timeout = TimeSpan.FromMinutes(60);
                HttpResponseMessage response = client.SendAsync(RequestMessage).Result;
                if (response.IsSuccessStatusCode)
                {
                    string result = await response.Content.ReadAsStringAsync();
                    return result;
                }
                else if (response.StatusCode == HttpStatusCode.Unauthorized)
                {
                    throw new UnauthorizedAccessException();
                }
            }

            return null;
        }

        private void SetOAuthToken()
        {
            if (OAuthToken != null)
            {
                RequestMessage.Headers.Add("Authorization", "Bearer " + OAuthToken);
            }
        }

        private void SetRequestBody()
        {
            if(RequestBody != null)
            {
                RequestMessage.Content = RequestBody;
            }
            
        }
    }
}