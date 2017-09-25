using PodfilterCore.Data;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using PodfilterCore.Models.PodcastModification;
using PodfilterCore.Models;
using System.Threading.Tasks;
using PodfilterCore.Exceptions.Repository;
using Microsoft.EntityFrameworkCore.ChangeTracking;

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
        public abstract Task<T> FindAsync(Predicate<T> predicat);
        public abstract IEnumerable<T> Where(Func<T, int, bool> predicate);

        public T Find(long id)
        {
            return ThrowIfNullOrReturn(Context.Find<T>(id));
        }

        public async Task<T> FindAsync(long id)
        {
            return ThrowIfNullOrReturn(await Context.FindAsync<T>(id));
        }

        public virtual T Persist(T toPersist)
        {
            var persisted = Context.Update(toPersist);
            Context.SaveChanges();
            return persisted.Entity;
        }

        public virtual async Task<T> PersistAsync(T toPersist)
        {
            var persisted = Context.Update(toPersist);
            await Context.SaveChangesAsync();
            return persisted.Entity;
        }

        public virtual IEnumerable<T> Persist(IEnumerable<T> toPersist)
        {
            var persisted = new List<EntityEntry<T>>();
            foreach (var item in toPersist)
                persisted.Add(Context.Update(item));
            Context.SaveChanges();
            return persisted.Select(x => x.Entity);
        }

        public virtual async Task<IEnumerable<T>> PersistAsync(IEnumerable<T> toPersist)
        {
            var persisted = new List<EntityEntry<T>>();
            foreach (var item in toPersist)
                persisted.Add(Context.Update(item));
            await Context.SaveChangesAsync();
            return persisted.Select(x => x.Entity);
        }

        protected T ThrowIfNullOrReturn(T entity)
        {
            if (entity == null)
                throw new EntityNotFoundException();
            else
                return entity;
        }

        protected IEnumerable<T> ThrowIfNullEmptyOrReturn(IEnumerable<T> entities)
        {
            if (entities == null || entities.Count() == 0)
                throw new EntityNotFoundException();
            else
                return entities;
        }
    }

    /*
    public class SqlBasePodcastModificationRepository : SqliteRepository<BasePodcastModification>
    {
        public SqlBasePodcastModificationRepository(PfContext context) 
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
            return ThrowIfNullOrReturn(Context.Modifications.Find(predicate));
        }

        public override async Task<BasePodcastModification> FindAsync(Predicate<BasePodcastModification> predicate)
        {
            return ThrowIfNullOrReturn(await Context.Modifications.FindAsync(predicate));
        }

        public override IEnumerable<BasePodcastModification> Where(Func<BasePodcastModification, int, bool> predicate)
        {
            return ThrowIfNullEmptyOrReturn(Context.Modifications.Where(predicate));
        }
    }
    */

    public class SqliteSavedPodcastsRepository : SqliteRepository<SavedPodcast>
    {
        public SqliteSavedPodcastsRepository(PfContext context) 
            : base(context)
        {
            //
        }

        public override SavedPodcast Persist(SavedPodcast toPersist)
        {
            SavedPodcast persisted = null;

            var dto = Context.Podcasts.Find(toPersist.Id);
            if (dto == null)
                dto = new SavedPodcastDto(toPersist);

            Context.Update(dto);
            toPersist.Id = dto.Id;
            Context.SaveChanges();
            return persisted;
        }

        public override Task<SavedPodcast> PersistAsync(SavedPodcast toPersist)
        {
            throw new NotImplementedException();
        }

        public override IEnumerable<SavedPodcast> Persist(IEnumerable<SavedPodcast> toPersist)
        {
            throw new NotImplementedException();
        }

        public override Task<IEnumerable<SavedPodcast>> PersistAsync(IEnumerable<SavedPodcast> toPersist)
        {
            throw new NotImplementedException();
        }

        public override IQueryable<SavedPodcast> All()
        {
            return Context.Podcasts.Select(x => x.SavedPodcast).AsQueryable();
        }

        public override SavedPodcast Find(Predicate<SavedPodcast> predicate)
        {
            return ThrowIfNullOrReturn(Context.Podcasts.Find(predicate)?.SavedPodcast);
        }

        public override async Task<SavedPodcast> FindAsync(Predicate<SavedPodcast> predicate)
        {
            return ThrowIfNullOrReturn((await Context.Podcasts.FindAsync(predicate))?.SavedPodcast);
        }

        public override IEnumerable<SavedPodcast> Where(Func<SavedPodcast, int, bool> predicate)
        {
            return ThrowIfNullEmptyOrReturn(Context.Podcasts.Select(dto => dto.SavedPodcast).Where(predicate));
        }
    }
}
