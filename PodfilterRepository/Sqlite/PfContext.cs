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
        internal virtual DbSet<ModificationDto> Modificastions { get; set; }
        internal virtual DbSet<BaseDbParameter> Parameters { get; set; }

        public PfContext(DbContextOptions options)
            : base(options)
        {
            //
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SavedPodcastDto>()
                .ToTable("SavedPodcastDtos");
            modelBuilder.Entity<SavedPodcastDto>()
                .HasMany<ModificationDto>(x => x.Modifications);
            modelBuilder.Entity<SavedPodcastDto>()
                .HasOne<SavedPodcast>(x => x.SavedPodcast);

            modelBuilder.Entity<ModificationDto>()
                .ToTable("ModificationDto")
                .HasMany<BaseDbParameter>(x => x.Parameters);

            modelBuilder.Entity<BaseDbParameter>()
                .ToTable("ModificationParameters")
                .HasDiscriminator<string>("valueType")
                .HasValue<BoolParameter>(typeof(BoolParameter).Name)
                .HasValue<IntParameter>(typeof(IntParameter).Name)
                .HasValue<LongParameter>(typeof(LongParameter).Name)
                .HasValue<StringParameter>(typeof(StringParameter).Name)
                .HasValue<IntFilterMethodParameter>(typeof(IntFilterMethodParameter).Name)
                .HasValue<StringFilterMethodParameter>(typeof(StringFilterMethodParameter).Name)
                .HasValue<DurationFilterMethodParameter>(typeof(DurationFilterMethodParameter).Name)
                .HasValue<DateTimeFilterMethodParameter>(typeof(DateTimeFilterMethodParameter).Name)
                .HasValue<RemoveDuplicateEpisodesMethodParameter>(typeof(RemoveDuplicateEpisodesMethodParameter).Name);
        }
    }
}
