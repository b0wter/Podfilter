namespace Podfilter.Models.ContentActions
{
    public class AddStringContentAction : IContentAction<string>
    {
        public string Prefix { get; protected set; }
        public string Suffix { get; protected set; }

        public AddStringContentAction(string prefix = null, string suffix = null)
        {
            this.Prefix = prefix;
            this.Suffix = suffix;
        }
        
        public string ModifyContent(string content)
        {
            if (content == null)
                return null;
            else
                return $"{Prefix}{content}{Suffix}";
        }
    }
}