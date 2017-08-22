using PodfilterCore.Data;
using System.Net.Http;
using System.Threading.Tasks;

namespace PodfilterWeb.Helpers
{
    public class HttpContentProvider<T> : IHttpContentProvider<T>
    {
        private HttpClient Client { get; }

        public HttpContentProvider()
        {
            this.Client = new HttpClient();
        }
        
        public async Task<HttpRequestResult<T>> LoadStringFromUrl(string url, IContentDeserializer<T> deserializer)
        {
            var responseMessage = await Client.GetAsync(url);

            var content = default(T);
            if (responseMessage.IsSuccessStatusCode)
            {
                var contentAsString = await responseMessage.Content.ReadAsStringAsync();
                content = deserializer.DeserializeString(contentAsString);
            }
            
            return new HttpRequestResult<T>(responseMessage, content);
        }
    }
}