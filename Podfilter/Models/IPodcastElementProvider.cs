using System.Xml.Linq;

namespace Podfilter.Models
{
    public interface IPodcastElementProvider<out TElementProvided, in TSelector>
    {
        TElementProvided GetElement(XDocument podcast, TSelector selector);
    }

    public interface IPodcastElementProvider<out TElementProvided> : IPodcastElementProvider<TElementProvided, string>
    {
        //
    }

    public interface IPodcastStringValueProvider : IPodcastElementProvider<string>
    {
        //
    }
}