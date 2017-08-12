using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Podfilter.Models.ContentActions
{
    public class ReplaceStringContentAction : BaseAction<string>
    {
        public string ToReplace { get; protected set; }
        public string ReplaceWith { get; protected set; }
        public override Type TargetType => typeof(string);

        public ReplaceStringContentAction(string toReplace,  string replaceWith)
        {
            this.ToReplace = toReplace;
            this.ReplaceWith = replaceWith;
        }

        public override string ModifyContent(string content)
        {
            if (ToReplace == null || ReplaceWith == null)
                throw new ArgumentException($"Trying to use a ReplaceStringContentAction with null in ToReplace or ReplaceWith.");
            if (ToReplace == "")
                throw new ArgumentException($"Cannot use a ReplaceStringContentAction with an empty 'toReplace'.");
            if (content == null)
                throw new ArgumentException($"Cannot use a ReplaceStringContentAction on a null string.");
            return content.Replace(ToReplace, ReplaceWith);
        }

        public override bool CanParse(Type t)
        {
            return typeof(string) == t.GetType();
        }

        protected override string ParseContent(string content)
        {
            return content;
        }

        protected override string SerializeContent(string content)
        {
            return content;
        }
    }
}
