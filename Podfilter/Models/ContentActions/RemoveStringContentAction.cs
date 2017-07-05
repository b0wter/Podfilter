using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Podfilter.Models.ContentActions
{
    public class RemoveStringContentAction : ReplaceStringContentAction
    {
        public RemoveStringContentAction(string toRemove)
            : base(toRemove, null)
        {
            this.ToReplace = toRemove;
            this.ReplaceWith = string.Empty;
        }
    }
}
