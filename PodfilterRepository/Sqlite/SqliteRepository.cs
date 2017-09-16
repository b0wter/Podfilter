using PodfilterCore.Data;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using PodfilterCore.Models.PodcastModification;
using PodfilterCore.Models;

namespace PodfilterRepository.Sqlite
{
    public abstract class SqliteRepository<T> : IBaseRepository<T> where T : class
    {
        protected PfContext Context { get; }

        public SqliteRepository(PfContext context)
        {
            this.Context = context;
        }

        public abstract IQueryable<T> All();
        public abstract T Find(Predicate<T> predicate);
        public abstract IEnumerable<T> Where(Func<T, int, bool> predicate);

        public T Find(Guid id)
        {
            return Context.Find<T>(id);
        }

        public void Persist(T toPersist)
        {
            Context.Update(toPersist);
            Context.SaveChanges();
        }

        public void Persist(IEnumerable<T> toPersist)
        {
            foreach (var item in toPersist)
                Context.Update(item);
            Context.SaveChanges();
        }
    }

    public class BasePodcastModificationRepository : SqliteRepository<BasePodcastModification>
    {
        public BasePodcastModificationRepository(PfContext context) 
            : base(context)
        {
            //
        }

        public override IQueryable<BasePodcastModification> All()
        {
            return Context.Modifications;
        }

        public override BasePodcastModification Find(Predicate<BasePodcastModification> predicate)
        {
            return Context.Modifications.Find(predicate);
        }

        public override IEnumerable<BasePodcastModification> Where(Func<BasePodcastModification, int, bool> predicate)
        {
            return Context.Modifications.Where(predicate);
        }
    }

    public class SavedPodcastsRepository : SqliteRepository<SavedPodcast>
    {
        public SavedPodcastsRepository(PfContext context) 
            : base(context)
        {
            //
        }

        public override IQueryable<SavedPodcast> All()
        {
            throw new NotImplementedException();
        }

        public override SavedPodcast Find(Predicate<SavedPodcast> predicate)
        {
            throw new NotImplementedException();
        }

        public override IEnumerable<SavedPodcast> Where(Func<SavedPodcast, int, bool> predicate)
        {
            throw new NotImplementedException();
        }
    }
}
