namespace Podfilter.Models.ContentActions
{
    public interface IContentAction<T>
    {
        T ModifyContent(T content);
    }
}