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
        internal virtual DbSet<SavedPodcastDto> Podcasts { get; set; }
        internal virtual DbSet<PodcastModificationDto> Modifications {get;set;}

        public PfContext(DbContextOptions options)
            : base(options)
        {
            //
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PodcastModificationDto>()
                .ToTable("PodcastModificationDtos");

            modelBuilder.Entity<SavedPodcastDto>()
                .ToTable("SavedPodcastDtos")
                .HasMany(podcast => podcast.ModificationDtos)
                .WithOne(modification => modification.SavedPodcastDto)
                .HasForeignKey(podcast => podcast.Id);

            // Dont map the Modifications in the SavedPodcast automatically since they need some manual attention.
            modelBuilder.Entity<SavedPodcast>().Ignore(podcast => podcast.Modifications);
        }
    }
}
