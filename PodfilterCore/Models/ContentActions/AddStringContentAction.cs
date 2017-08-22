using System;

namespace PodfilterCore.Models.ContentActions
{
    public class AddStringContentAction : BaseAction<string>
    {
        public string Prefix { get; protected set; }
        public string Suffix { get; protected set; }
        public override Type TargetType => typeof(string);
        
        public AddStringContentAction(string prefix = null, string suffix = null)
        {
            this.Prefix = prefix;
            this.Suffix = suffix;
        }
        
        public override string ModifyContent(string content)
        {
            if (content == null)
                return null;
            else
                return $"{Prefix}{content}{Suffix}";
        }

        public override bool CanParse(Type t)
        {
            var isMatch = t == typeof(string);
            return isMatch;
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