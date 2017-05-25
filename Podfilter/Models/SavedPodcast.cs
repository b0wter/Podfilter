using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Podfilter.Models
{
    /// <summary>
    /// Database model for a user-saved podcast.
    /// </summary>
    public class SavedPodcast
    {
        /// <summary>
        /// Default time a podcast is considered to be current (in seconds).
        /// </summary>
        public const int DEFAULT_CACHE_LIFETIME = 60 * 60 * 4;
        /// <summary>
        /// List of filters that are applied to the podcast.
        /// </summary>
        public List<IFilter> Filters { get; } = new List<IFilter>();
        /// <summary>
        /// Url of the original podcast.
        /// </summary>
        public string Url { get; private set; } = string.Empty;
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
        public int CacheLifeTime { get; set; } = DEFAULT_CACHE_LIFETIME;
        
        public SavedPodcast(string url)
        {
            this.Url = url;
        }

        /// <summary>
        /// Updates the feed from the remote source and runs the filters. Updates <see cref="LastUpdated"/> and 
        /// <see cref="CachedFeed"/>.
        /// </summary>
        /// <returns></returns>
        private async Task Update()
        {
            
        }

        /// <summary>
        /// Either returns <see cref="CachedFeed"/> or downloads the feed from <see cref="Url"/> and applies 
        /// the <see cref="Filters"/>.
        /// </summary>
        /// <returns></returns>
        public async Task<string> GetCurrentFeed()
        {
            
        }
    }
}