﻿using Microsoft.EntityFrameworkCore;
using WorldCities.Data.Models;

namespace WorldCities.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext() : base()
        {
        }

        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        /// <summary>
        /// IMPORTANT NOTE: the following method override is redundant 
        /// (since we've already configured our entities using Data Annotations)
        /// and has been left there for demonstration purposes only.
        /// See "Entity Types configuration methods" in Chapter 4 for details.
        /// </summary>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<City>().ToTable("Cities");
            modelBuilder.Entity<City>()
                .HasKey(x => x.Id);
            modelBuilder.Entity<City>()
                .Property(x => x.Id).IsRequired();

            modelBuilder.Entity<Country>().ToTable("Countries");
            modelBuilder.Entity<Country>()
                .HasKey(x => x.Id);
            modelBuilder.Entity<Country>()
                .Property(x => x.Id).IsRequired();
            modelBuilder.Entity<City>()
                .HasOne(x => x.Country)
                .WithMany(y => y.Cities)
                .HasForeignKey(x => x.CountryId);

            // add the EntityTypeConfiguration classes
            modelBuilder.ApplyConfigurationsFromAssembly(
                typeof(ApplicationDbContext).Assembly
                );
        }

        public DbSet<City> Cities { get; set; }
        public DbSet<Country> Countries { get; set; }
    }
}
