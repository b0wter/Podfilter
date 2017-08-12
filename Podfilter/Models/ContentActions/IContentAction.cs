using System;

namespace Podfilter.Models.ContentActions
{
    public interface IContentAction
    {
        /// <summary>
        /// Returns wether content of the given type can be used with this action.
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        bool CanParse(Type t);
        
        /// <summary>
        /// Parses <paramref name="content"/> as typeof T and calls ModifyContent of the derived interface.
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        string ParseAndModifyContent(string content);
        
        /// <summary>
        /// Type of content that this IContentAction is suitable for (e.g. string, DateTime,...).
        /// </summary>
        Type TargetType { get; }
    }

    public interface IContentAction<T> : IContentAction
    {
        /// <summary>
        /// Applies the action to the given content.
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        T ModifyContent(T content);
    }
}