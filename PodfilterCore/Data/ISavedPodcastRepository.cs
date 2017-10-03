using PodfilterCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PodfilterCore.Data
{
    public interface ISavedPodcastRepository
    {
        IQueryable<SavedPodcast> All();
        SavedPodcast Find(long id);
        SavedPodcast Find(Predicate<SavedPodcast> predicate);
        Task<SavedPodcast> FindAsync(long id);
        Task<SavedPodcast> FindAsync(Predicate<SavedPodcast> predicate);
        IEnumerable<SavedPodcast> Persist(IEnumerable<SavedPodcast> toPersist);
        SavedPodcast Persist(SavedPodcast toPersist);
        Task RemoveAsync(long id);
        Task RemoveAsync(SavedPodcast entity);
        void UpdateLastUsed(SavedPodcast podcast);
        IEnumerable<SavedPodcast> Where(Func<SavedPodcast, int, bool> predicate);
    }
}
