using Microsoft.EntityFrameworkCore;
using PodfilterCore.Models;
using PodfilterCore.Models.PodcastModification;
using System;
using System.Collections.Generic;
using System.Text;
using PodfilterCore.Models.ContentActions;
using PodfilterRepository.Dtos;

namespace PodfilterRepository.Sqlite
{
    public class PfContext : DbContext
    {
        internal DbSet<SavedPodcastDto> Podcasts { get; set; }
        internal DbSet<BasePodcastModificationDto> Modifications {get;set;}

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

            modelBuilder.Entity<BasePodcastModificationDto>()
                .HasDiscriminator<string>("ModificationTypeDtos")
                .HasValue<EpisodeTitleFilterModificationDto>("EpisodeTitleFilter")
                .HasValue<EpisodeDescriptionFilterModificationDto>("EpisodeDescriptionFilters")
                .HasValue<EpisodeDurationFilterModificationDto>("EpisodeDurationFilters")
                .HasValue<EpisodePublishDateFilterModificationDto>("EpisodePublishDateFilters")
                .HasValue<RemoveDuplicateEpisodesModificationDto>("RemoveDuplicateEpisodes");


            modelBuilder.Entity<SavedPodcastDto>()
                .ToTable("SavedPodcastsDtos")
                .HasMany<BasePodcastModificationDto>(podcast => podcast.ModificationDtos)
                .WithOne(modification => modification.SavedPodcast)
                .HasForeignKey(podcast => podcast.Id);
        }
    }
}
