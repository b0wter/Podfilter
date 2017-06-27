namespace Podfilter.Helpers
{
    public interface IContentDeserializer<out T>
    {
        T DeserializeString(string s);
    }

    public class StringContentDeserializer : IContentDeserializer<string>
    {
        public string DeserializeString(string s)
        {
            return s;
        }
    }
}