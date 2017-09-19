using Microsoft.EntityFrameworkCore;
using PodfilterCore.Models;
using PodfilterCore.Models.PodcastModification;
using System;
using System.Collections.Generic;
using System.Text;

namespace PodfilterRepository.Sqlite
{
    public class PfContext : DbContext
    {
        public virtual DbSet<BasePodcastModification> Modifications { get; set; }
        public virtual DbSet<SavedPodcast> Podcasts { get; set; }

        public PfContext(DbContextOptions options)
            : base(options)
        {
            //
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SavedPodcast>().ToTable("savedPodcastDtos");
            modelBuilder.Entity<BasePodcastModification>().ToTable("podcastModifications");

            modelBuilder.Entity<SavedPodcast>().HasMany<BasePodcastModification>(podcast => podcast.Modifications);
        }
    }
}
