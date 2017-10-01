using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PodfilterCore.Models.PodcastModification;
using System.Linq;
using System.ComponentModel.DataAnnotations.Schema;

namespace PodfilterCore.Models
{
    /// <summary>
    /// Database model for a user-saved podcast.
    /// </summary>
    public class SavedPodcast
    {
        /// <summary>
        /// Default time a podcast is considered to be current (in seconds).
        /// </summary>
        public const int DefaultCacheLifetime = 60 * 60 * 4;
        /// <summary>
        /// Default lifetime of a stored podcast. If it is not used within this time it will be deleted.
        /// </summary>
        public const int DefaultStorageLifetime = 30 * 24 * 60 * 60;
        /// <summary>
        /// Primary key for database and identification purposes.
        /// </summary>
        public long Id { get; set; }
        /// <summary>
        /// List of filters that are applied to the podcast.
        /// </summary>
        [NotMapped]
        public ICollection<BasePodcastModification> Modifications { get; set; }
        /// <summary>
        /// Url of the original podcast.
        /// </summary>
        public string Url { get; set; } = string.Empty;
        /// <summary>
        /// Serialized, filtered string.
        /// </summary>
        public string CachedFeed { get; set; } = string.Empty;
        /// <summary>
        /// Time when <see cref="CachedFeed"/> was created.
        /// </summary>
        public DateTime LastUpdated { get; set; } = DateTime.MinValue;
        /// <summary>
        /// Time a podcast is considered to be current. If <see cref="LastUpdated"/> + <see cref="CacheLifeTime"/>
        /// is greater than <see cref="DateTime.Now"/> a new feed will be downloaded and parsed on the next request.
        /// </summary>
        public int CacheLifeTime { get; set; } = DefaultCacheLifetime;
        /// <summary>
        /// Last time this podcast was used.
        /// </summary>
        public DateTime LastUsed { get; set; }

        /// <summary>
        /// Updates the feed from the remote source and runs the filters. Updates <see cref="LastUpdated"/> and 
        /// <see cref="CachedFeed"/>.
        /// </summary>
        /// <returns></returns>
        private void Update()
        {
            throw new NotImplementedException(); 
        }

        /// <summary>
        /// Either returns <see cref="CachedFeed"/> or downloads the feed from <see cref="Url"/> and applies 
        /// the <see cref="Filters"/>.
        /// </summary>
        /// <returns></returns>
        public string GetCurrentFeed()
        {
            throw new NotImplementedException();
        }
    }
}