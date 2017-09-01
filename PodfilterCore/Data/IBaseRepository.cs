using PodfilterCore.Models.PodcastModification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PodfilterCore.Data
{
    public interface IBaseRepository<T>
    {
        IQueryable<T> All();
        T Find(Predicate<T> predicate);
        IEnumerable<T> Where(Func<T, int, bool> predicate);
        void Persist(T toPersist);
        void Persist(IEnumerable<T> toPersist);
    }
}
