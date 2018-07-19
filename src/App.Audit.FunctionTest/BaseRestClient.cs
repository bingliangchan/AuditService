using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace App.Audit.FunctionTest
{
    public class BaseRestClient
    {

        protected async Task<HttpResponseMessage> GetService(string url)
        {
            var httpClient = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Get, url);

            return await SendAsyncRequest(httpClient, request);
        }

        protected async Task<HttpResponseMessage> PostService(string url, HttpContent content)
        {
            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var request = new HttpRequestMessage(HttpMethod.Post, url);

            request.Content = content;

            return await SendAsyncRequest(httpClient, request);
        }

        protected async Task<HttpResponseMessage> PutService(string url, HttpContent content)
        {
            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var request = new HttpRequestMessage(HttpMethod.Put, url);

            request.Content = content;

            return await SendAsyncRequest(httpClient, request);
        }

        protected async Task<HttpResponseMessage> DeleteService(string url)
        {
            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var request = new HttpRequestMessage(HttpMethod.Delete, url);

            return await SendAsyncRequest(httpClient, request);
        }

        private async Task<HttpResponseMessage> SendAsyncRequest(HttpClient httpClient, HttpRequestMessage request)
        {
            var response = await httpClient.SendAsync(request);

            if (!response.IsSuccessStatusCode)
                throw new Exception(response.StatusCode.ToString());
            return response;
        }

    }
}