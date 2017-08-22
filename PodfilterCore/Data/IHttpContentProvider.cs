using System.Threading.Tasks;

namespace PodfilterCore.Data
{
    public interface IHttpContentProvider<T>
    {
        Task<HttpRequestResult<T>> LoadStringFromUrl(string url, IContentDeserializer<T> deserializer);
    }
}