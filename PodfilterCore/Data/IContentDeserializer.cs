namespace PodfilterCore.Data
{
    public interface IContentDeserializer<out T>
    {
        T DeserializeString(string s);
    }
}