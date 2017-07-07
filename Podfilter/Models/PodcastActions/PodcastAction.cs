using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using Podfilter.Models.ContentActions;
using System;
using System.Linq;

namespace Podfilter.Models.PodcastActions
{
    public abstract class PodcastAction : IPodcastAction
    {
        /// <summary>
        /// Type of content that the targeted (<see cref="XPath"/>) element contains.
        /// </summary>
        protected abstract Type TargetElementType { get; }
        
        /// <summary>
        /// List of actions performed on the content.
        /// </summary>
        protected List<IContentAction> Actions { get; set; } = new List<IContentAction>();
        
        /// <summary>
        /// Modifies the content of all matching elements for the given podcast.
        /// </summary>
        /// <param name="podcast"></param>
        /// <returns></returns>
        public XDocument PerformAction(XDocument podcast)
        {
            var matchingItems = GetMatchingElements(podcast);
            
            foreach (var item in matchingItems)
            {
                var matchingActions = Actions.Where(a => a.CanParse(TargetElementType)).ToList();
                matchingActions.ForEach(a => {
                    item.Value = a.ParseAndModifyContent(item.Value);
                });
            }

            return podcast;
        }
        
        /// <summary>
        /// Retrieves all elements that this action matches.
        /// </summary>
        /// <param name="podcast"></param>
        /// <returns></returns>
        protected abstract IEnumerable<XElement> GetMatchingElements(XDocument podcast);
    }
}