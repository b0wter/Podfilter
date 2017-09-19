using PodfilterCore.Models.PodcastModification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PodfilterCore.Data
{
    public interface IBaseRepository<T> where T : class
    {
        IQueryable<T> All();

        T Find(long primaryKey);
        Task<T> FindAsync(long primaryKey);

        T Find(Predicate<T> predicate);
        Task<T> FindAsync(Predicate<T> predicate);

        // There is no async where because its Lynq and only an expression tree is build.
        IEnumerable<T> Where(Func<T, int, bool> predicate);

        Task<EntityEntry<T>> Persist(T toPersist);
        Task PersistAsync(T toPersist);

        void Persist(IEnumerable<T> toPersist);
        Task PersistAsync(IEnumerable<T> toPersist);
    }
}
