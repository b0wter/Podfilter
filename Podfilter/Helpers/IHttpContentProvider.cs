using System.Threading.Tasks;

namespace Podfilter.Helpers
{
    public interface IHttpContentProvider<T>
    {
        Task<HttpRequestResult<T>> LoadStringFromUrl(string url, IContentDeserializer<T> deserializer);
    }
}