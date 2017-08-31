using System;

namespace PodfilterCore.Models.ContentActions
{
    public class ReplaceExistingStringContentAction : AddStringContentAction
    {
        public string NewString {get; private set;}
        
        public ReplaceExistingStringContentAction(string newString)
            : base(null, null)
        {
            NewString = newString;
        }

        public override string ModifyContent(string content)
        {
            return NewString;
        }
    }
}