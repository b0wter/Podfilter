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
    }

    public interface IContentAction<T> : IContentAction
    {
        /// <summary>
        /// Applies the action to the given content.
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        T ModifyContent(T content);

        /// <summary>
        /// Parses <paramref name="content"/> as typeof T and calls <see cref="ModifyContent(T)"/>.
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        T ParseAndModifyContent(string content);
    }
}