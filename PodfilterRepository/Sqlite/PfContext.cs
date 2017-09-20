using Microsoft.EntityFrameworkCore;
using PodfilterCore.Models;
using PodfilterCore.Models.PodcastModification;
using System;
using System.Collections.Generic;
using System.Text;
using PodfilterCore.Models.PodcastModification.Filters;
using PodfilterCore.Models.PodcastModification.Others;

namespace PodfilterRepository.Sqlite
{
    public class PfContext : DbContext
    {
        public DbSet<SavedPodcast> Podcasts { get; set; }
        public DbSet<BasePodcastModification> Modifications {get;set;}
        //public virtual DbSet<EpisodeTitleFilterModification> EpisodeTitleFilterModifications {get;set;}
        //public virtual DbSet<EpisodeDescriptionFilterModification> EpisodeDescriptionFilterModifications {get;set;}
        //public virtual DbSet<EpisodeDurationFilterModification> EpisodeDurationFilterModifications {get;set;}
        //public virtual DbSet<EpisodePublishDateFilterModification> EpisodePublishDateFilterModifications {get;set;}
        //public virtual DbSet<RemoveDuplicateEpisodesModification> RemoveDuplicateEpisodesModifications {get;set;}

        public PfContext(DbContextOptions options)
            : base(options)
        {
            //
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<EpisodeTitleFilterModification>().ToTable("EpisodeTitleFilters");
            //modelBuilder.Entity<EpisodeDescriptionFilterModification>().ToTable("EpisodeDescriptionFilters");
            //modelBuilder.Entity<EpisodeDurationFilterModification>().ToTable("EpisodeDurationFilters");
            //modelBuilder.Entity<EpisodePublishDateFilterModification>().ToTable("EpisodePublishDateFilters");
            modelBuilder.Entity<BasePodcastModification>()
                .HasDiscriminator<string>("ModificationType")
                .HasValue<EpisodeTitleFilterModification>("EpisodeTitleFilter")
                .HasValue<EpisodeDescriptionFilterModification>("EpisodeDescriptionFilters")
                .HasValue<EpisodeDurationFilterModification>("EpisodeDurationFilters")
                .HasValue<EpisodePublishDateFilterModification>("EpisodePublishDateFilters");

            modelBuilder.Entity<SavedPodcast>()
                .ToTable("SavedPodcasts")
                .HasMany<BasePodcastModification>(podcast => podcast.Modifications)
                .WithOne(modification => modification.SavedPodcast)
                .HasForeignKey(podcast => podcast.Id);
        }
    }
}
