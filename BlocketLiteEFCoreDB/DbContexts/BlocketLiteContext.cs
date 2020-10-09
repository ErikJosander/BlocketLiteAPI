using BlocketLiteEFCoreDB.Entities;
using BlocketLiteEFCoreDB.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BlocketLiteEFCoreDB.DbContexts
{
    /// <summary>
    /// A Context Class that inherits from <see cref="DbContext"/>. <see cref="BlocketLiteContext"/> also seeds and implements rules for the DB.
    /// </summary>
    public class BlocketLiteContext : IdentityDbContext<User, IdentityRole<string>, string>
    {
        public BlocketLiteContext(DbContextOptions<BlocketLiteContext> options)
          : base(options)
        {

        }

        /// <summary>
        /// A <see cref="DbSet{TEntity}"/> of <see cref="Advertisement"/>
        /// </summary>
        public DbSet<Advertisement> Advertisements { get; set; }

        /// <summary>
        /// A <see cref="DbSet{TEntity}"/> of <see cref="Comment"/>
        /// </summary>
        public DbSet<Comment> Comments { get; set; }


        /// <summary>
        /// A <see cref="DbSet{TEntity}"/> of <see cref="PropertyType"/>
        /// </summary>
        public DbSet<PropertyType> PropertyTypes { get; set; }

        /// <summary>
        /// A <see cref="DbSet{TEntity}"/> of <see cref="Rating"/>
        /// </summary>
        public DbSet<Rating> Ratings { get; set; }

        /// <summary>
        /// Seeds the DB with dummy data on creation.
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
            .HasIndex(u => u.Email)
            .IsUnique();

            modelBuilder.Entity<User>()
                .HasIndex(u => u.UserName)
                .IsUnique();

            modelBuilder.Entity<Rating>()
                .HasOne<User>(s => s.RatedUser)
                .WithMany(g => g.Ratings)
                .HasForeignKey(c => c.RatingUserId)
                .OnDelete(DeleteBehavior.SetNull);

            // Provides all seed data
            modelBuilder.CreatePropertyTypeData();
            modelBuilder.CreateDevSeedData();
            

            base.OnModelCreating(modelBuilder);
        }
    }
}
