using PodfilterCore.Data;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using PodfilterCore.Models.PodcastModification;

namespace PodfilterRepository.Sqlite
{
    public abstract class SqliteRepository<T> : IBaseRepository<T>
    {
        protected PfContext Context { get; }

        public SqliteRepository(PfContext context)
        {
            this.Context = context;
        }

        public abstract IQueryable<T> All();
        public abstract T Find(Predicate<T> predicate);
        public abstract IEnumerable<T> Where(Func<T, int, bool> predicate);
        public abstract void Persist(T toPersist);
        public abstract void Persist(IEnumerable<T> toPersist);
    }

    public class BasePodcastModificationRepository : SqliteRepository<BasePodcastModification>
    {
        public override IQueryable<BasePodcastModification> All()
        {
            
        }

        public override BasePodcastModification Find(Predicate<BasePodcastModification> predicate)
        {
            throw new NotImplementedException();
        }

        public override void Persist(BasePodcastModification toPersist)
        {
            throw new NotImplementedException();
        }

        public override void Persist(IEnumerable<BasePodcastModification> toPersist)
        {
            throw new NotImplementedException();
        }

        public override IEnumerable<BasePodcastModification> Where(Func<BasePodcastModification, int, bool> predicate)
        {
            throw new NotImplementedException();
        }
    }
}
