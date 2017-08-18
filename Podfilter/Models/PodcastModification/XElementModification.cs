using System;
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
        public abstract Type TargetType { get; }
    }

    /// <summary>
    /// Modifies the <see cref="XElement.Value"/> of a podcast element using the given action.
    /// </summary>
    public class XElementActionModification : XElementModification
    {
        private readonly IContentAction _action;
        public override Type TargetType => null;

        public XElementActionModification(IContentAction modification)
        {
            _action = modification;
        }
        
        public override XElement Modify(XElement element)
        {
            if (element != null)
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
        public override Type TargetType => _filter?.TargetType;

        public XElementFilterModification(IContentFilter filter)
        {
            _filter = filter;
        }
        
        public override XElement Modify(XElement element)
        {
            if (element == null)
                throw new ArgumentNullException();

            bool passesFilter = _filter.PassesFilter(element.Value);
            if (passesFilter)
                return element;
            else
            {
                element.Parent?.Remove();
                return null;
            }
        }
    }
}