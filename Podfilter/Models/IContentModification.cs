using System.Xml.Linq;

namespace Podfilter.Models
{
    public interface IContentModification
    {
        void ModifyContent(XDocument document);
    }
}