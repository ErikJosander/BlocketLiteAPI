using BlocketLiteEFCoreDB.Entities;
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
    public class BlocketLiteContext : IdentityDbContext<User>
    {
        // Constructor
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

            modelBuilder.Entity<PropertyType>().HasData(
                 new PropertyType()
                 {
                     Id = 1,
                     Type = "apartment"
                 },
                 new PropertyType()
                 {
                     Id = 2,
                     Type = "house"
                 },
                 new PropertyType()
                 {
                     Id = 3,
                     Type = "office"
                 },
                 new PropertyType()
                 {
                     Id = 4,
                     Type = "warehouse"
                 });


            var environment = (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT"));
            if (environment == "Development")
            {
                List<User> users = new List<User>();
                User user = new User()
                {
                    UserName = "Calle",
                    Email = "Calle@gmail.com",
                    Password = "123"
                };
                users.Add(user);

                user = new User()
                {
                    UserName = "Erik",
                    Email = "Erik@gmail.com",
                    Password = "123"
                };
                users.Add(user);

                modelBuilder.Entity<User>().HasData(users);


                modelBuilder.Entity<Advertisement>().HasData(
                    new Advertisement()
                    {
                        Id = 1,
                        Title = "Cool apartment for rent",
                        Description = "Big apartment with 5 rooms for rent",
                        ConstructionYear = 1978,
                        UserId = users.Where(u => u.UserName == "Calle").FirstOrDefault().Id,
                        CreatedOn = Helpers.GetCurrentDateUTC.GetDateTimeUTC(),
                        SellingPrice = null,
                        RentingPrice = 350,
                        PropertyTypeId = 1,
                        CanBeSold = false,
                        CanBeRented = true,
                        Contact = "10708 001122",
                        Address = "Varberg"
                    });


                modelBuilder.Entity<Comment>().HasData(
                    new Comment
                    {
                        Id = 1,
                        AdvertisementId = 1,
                        Content = "First comment by Calle",
                        CreatedOn = DateTime.Now,
                        UserId = users.Where(u => u.UserName == "Calle").FirstOrDefault().Id,
                        UserName = "Calle"
                    });


                modelBuilder.Entity<Rating>().HasData(
                    new Rating
                    {
                        Id = 1,
                        Value = 3,
                        RatedUserId = users.Where(u => u.UserName == "Calle").FirstOrDefault().Id,
                        RatingUserId = users.Where(u => u.UserName == "Erik").FirstOrDefault().Id,
                    });             
            }       

            base.OnModelCreating(modelBuilder);
        }
    }
}
