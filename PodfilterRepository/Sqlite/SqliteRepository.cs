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
using Microsoft.EntityFrameworkCore;

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

        public virtual T Find(long id)
        {
            return ThrowIfNullOrReturn(Context.Find<T>(id));
        }

        public virtual async Task<T> FindAsync(long id)
        {
            return ThrowIfNullOrReturn(await Context.FindAsync<T>(id));
        }

        public abstract Task RemoveAsync(long id);

        public abstract Task RemoveAsync(T entity);

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

        public IEnumerable<T> Persist(IEnumerable<T> toPersist)
        {
            var persisted = new List<EntityEntry<T>>();
            foreach (var item in toPersist)
                persisted.Add(Context.Update(item));
            Context.SaveChanges();
            return persisted.Select(x => x.Entity);
        }

        public async Task<IEnumerable<T>> PersistAsync(IEnumerable<T> toPersist)
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

    public class SqliteSavedPodcastsRepository : SqliteRepository<SavedPodcast>
    {
        public SqliteSavedPodcastsRepository(PfContext context)
            : base(context)
        {
            //
        }

        public override SavedPodcast Persist(SavedPodcast toPersist)
        {
            var dto = Context.Podcasts.Find(toPersist.Id);
            if (dto == null)
            {
                dto = new SavedPodcastDto(toPersist);
                Context.Podcasts.Add(dto);
            }

            if (dto.Modifications != null)
                Context.RemoveRange(dto.Modifications);

            dto.Modifications = toPersist.Modifications.Select(x => new ModificationDto(x)).ToList();
            Context.SaveChanges();
            toPersist.Id = dto.Id;
            return toPersist;
        }

        public override IQueryable<SavedPodcast> All()
        {
            return Context.Podcasts.Select(x => x.SavedPodcast).AsQueryable();
        }

        public override SavedPodcast Find(long id)
        {
            var dto = Context.Podcasts
                .Include(x => x.SavedPodcast)
                .Include(x => x.Modifications).ThenInclude(x => x.Parameters)
                .Where(x => x.Id == id)
                .SingleOrDefault();
            return FillSavedPodcastFromDto(dto);
        }

        public override SavedPodcast Find(Predicate<SavedPodcast> predicate)
        {
            var dto = Context.Podcasts.Find(predicate);
            return FillSavedPodcastFromDto(dto);
        }

        public override async Task<SavedPodcast> FindAsync(long id)
        {
            var dto = await Context.Podcasts.FindAsync(id);
            return FillSavedPodcastFromDto(dto);
        }

        public override async Task<SavedPodcast> FindAsync(Predicate<SavedPodcast> predicate)
        {
            var dto = await Context.Podcasts.FindAsync(predicate);
            return FillSavedPodcastFromDto(dto);
        }

        private SavedPodcast FillSavedPodcastFromDto(SavedPodcastDto dto)
        {
            if (dto == null)
                throw new EntityNotFoundException();

            var modifications = dto.Modifications.Select(x => x.ToModification());
            dto.SavedPodcast.Modifications = modifications.ToList();

            return dto.SavedPodcast;
        }

        public override IEnumerable<SavedPodcast> Where(Func<SavedPodcast, int, bool> predicate)
        {
            return ThrowIfNullEmptyOrReturn(Context.Podcasts.Select(dto => dto.SavedPodcast).Where(predicate));
        }

        public override async Task RemoveAsync(long id)
        {
            var savedPodcast = Context.Podcasts
                                .Include(x => x.SavedPodcast)
                                .Include(x => x.Modifications).ThenInclude(x => x.Parameters)
                                .Where(x => x.Id == id)
                                .SingleOrDefault();
            Context.Remove(savedPodcast);
            await Context.SaveChangesAsync();
        }

        public override async Task RemoveAsync(SavedPodcast entity)
        {
            if (entity != null && Context.Podcasts.Any(x => x.Id == entity.Id))
            {
                await RemoveAsync(entity.Id);
            }
            else
            {
                Context.Remove(entity);
                await Context.SaveChangesAsync();
            }
        }
    }
}
