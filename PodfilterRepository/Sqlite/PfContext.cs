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

        public PfContext(DbContextOptions options)
            : base(options)
        {
            //
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SavedPodcastDto>()
                .ToTable("SavedPodcastDtos");
        }
    }
}
