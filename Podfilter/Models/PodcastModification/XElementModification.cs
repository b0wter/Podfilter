using System.Xml.Linq;
using Podfilter.Models.ContentActions;
using Podfilter.Models.ContentFilters;

namespace Podfilter.Models.PodcastModification
{
    /// <summary>
    /// Is used to be able to apply <see cref="IContentAction"/>s and <see cref="IContentFilter"/>s in the same way.
    /// </summary>
    public abstract class XElementModification
    {
        public abstract XElement Modify(XElement element);
    }

    /// <summary>
    /// Modifies the <see cref="XElement.Value"/> of a podcast element using the given action.
    /// </summary>
    public class XElementActionModification : XElementModification
    {
        private readonly IContentAction _action;

        public XElementActionModification(IContentAction modification)
        {
            _action = modification;
        }
        
        public override XElement Modify(XElement element)
        {
            element.Value = _action.ParseAndModifyContent(element.Value);
            return element;
        }
    }

    /// <summary>
    /// Removes an XElement from if parent node if it fails the given filter.
    /// </summary>
    public class XElementFilterModification : XElementModification
    {
        private readonly IContentFilter _filter;

        public XElementFilterModification(IContentFilter filter)
        {
            _filter = filter;
        }
        
        public override XElement Modify(XElement element)
        {
            if (_filter.PassesFilter(element.Value))
                return element;
            else
            {
                element.Parent?.Remove();
                return null;
            }
        }
    }
}