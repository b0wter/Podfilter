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

        public virtual IEnumerable<T> Persist(IEnumerable<T> toPersist)
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

    public class SqliteSavedPodcastsRepository : SqliteRepository<SavedPodcast>, ISavedPodcastRepository
    {
        public SqliteSavedPodcastsRepository(PfContext context)
            : base(context)
        {
            //
        }

        /// <summary>
        /// Persists the <paramref name="toPersist"/> in the database. This creates or updates the entity. Note that all modifications/parameters are recreated in the db as well (increasing their Ids).
        /// </summary>
        /// <param name="toPersist"></param>
        /// <returns></returns>
        public override SavedPodcast Persist(SavedPodcast toPersist)
        {
            RemoveOldSavedPodcastDtosIfOverMaximum();

            var dto = Context.Podcasts.Find(toPersist.Id);
            if (dto == null)
            {
                dto = new SavedPodcastDto(toPersist);
                Context.Podcasts.Add(dto);
            }

            if (dto.Modifications != null)
            {
                Context.RemoveRange(dto.Modifications);
                Context.RemoveRange(dto.Modifications.SelectMany(x => x.Parameters));
            }

            dto.Modifications = toPersist.Modifications.Select(x => new ModificationDto(x)).ToList();
            Context.SaveChanges();
            toPersist.Id = dto.Id;
            return toPersist;
        }

        /// <summary>
        /// Persists the <paramref name="toPersist"/> entities by calling <see cref="Persist(SavedPodcast)"/> for each entity.
        /// </summary>
        /// <param name="toPersist"></param>
        /// <returns></returns>
        public override IEnumerable<SavedPodcast> Persist(IEnumerable<SavedPodcast> toPersist)
        {
            var persisted = new List<SavedPodcast>(toPersist.Count());
            foreach (var entity in toPersist)
                persisted.Add(Persist(entity));
            return persisted;
        }

        /// <summary>
        /// If number of <see cref="SavedPodcast"/>s exceeds <see cref="SavedPodcastDto.MaximumSavedPodcastCount"/> removes the least used podcasts.
        /// </summary>
        private void RemoveOldSavedPodcastDtosIfOverMaximum()
        {
            var surplusAmount = Math.Max(0, Context.Podcasts.Count() - SavedPodcastDto.MaximumSavedPodcastCount);
            if (surplusAmount == 0)
                return;

            var surplusPodcasts = Context.Podcasts
                                    .Include(x => x.Modifications).ThenInclude(x => x.Parameters)
                                    .Include(x => x.SavedPodcast)
                                    .OrderBy(x => x.SavedPodcast.LastUsed).Take(surplusAmount);

            Context.RemoveRange(surplusPodcasts);
            Context.SaveChanges();
        }

        /// <summary>
        /// Returns all <see cref="SavedPodcast"/>s, including their modifications and parameters.
        /// </summary>
        /// <returns></returns>
        public override IQueryable<SavedPodcast> All()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Updates the <see cref="SavedPodcast.LastUsed"/> property with the current time.
        /// </summary>
        /// <param name="podcast"></param>
        public void UpdateLastUsed(SavedPodcast podcast)
        {
            var newTime = DateTime.Now;
            podcast.LastUsed = newTime;
            Context.Podcasts.Include(x => x.SavedPodcast).First(x => x.SavedPodcast == podcast).SavedPodcast.LastUsed = newTime;
            Context.SaveChanges();
        }

        /// <summary>
        /// Returns a <see cref="SavedPodcast"/> based on its id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public override SavedPodcast Find(long id)
        {
            var dto = Context.Podcasts
                .Include(x => x.SavedPodcast)
                .Include(x => x.Modifications).ThenInclude(x => x.Parameters)
                .Where(x => x.Id == id)
                .SingleOrDefault();
            return FillSavedPodcastFromDto(dto);
        }

        /// <summary>
        /// Returns a <see cref="SavedPodcast"/> based on the given <paramref name="predicate"/>.
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public override SavedPodcast Find(Predicate<SavedPodcast> predicate)
        {
            var dto = Context.Podcasts.Find(predicate);
            return FillSavedPodcastFromDto(dto);
        }

        /// <summary>
        /// Returns a <see cref="SavedPodcast"/> based on its id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public override async Task<SavedPodcast> FindAsync(long id)
        {
            var dto = await Context.Podcasts.FindAsync(id);
            return FillSavedPodcastFromDto(dto);
        }

        /// <summary>
        /// Returns a <see cref="SavedPodcast"/> based on the given <paramref name="predicate"/>.
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public override async Task<SavedPodcast> FindAsync(Predicate<SavedPodcast> predicate)
        {
            var dto = await Context.Podcasts.FindAsync(predicate);
            return FillSavedPodcastFromDto(dto);
        }

        /// <summary>
        /// Fills the <see cref="SavedPodcast"/> with the corresponding modifications and parameters.
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        private SavedPodcast FillSavedPodcastFromDto(SavedPodcastDto dto)
        {
            if (dto == null)
                throw new EntityNotFoundException();

            var modifications = dto.Modifications.Select(x => x.ToModification());
            dto.SavedPodcast.Modifications = modifications.ToList();

            return dto.SavedPodcast;
        }

        /// <summary>
        /// Returns the <see cref="SavedPodcast"/>s matching the given <paramref name="predicate"/>.
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public override IEnumerable<SavedPodcast> Where(Func<SavedPodcast, int, bool> predicate)
        {
            return ThrowIfNullEmptyOrReturn(Context.Podcasts.Select(dto => dto.SavedPodcast).Where(predicate));
        }

        /// <summary>
        /// Removes a savedpodcast, its modifications and parameters based on its id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Removes the <paramref name="entity"/> and its modifications and parameters.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
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
