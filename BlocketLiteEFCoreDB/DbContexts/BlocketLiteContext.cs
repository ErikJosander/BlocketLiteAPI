using BlocketLiteEFCoreDB.Entities;
using Microsoft.EntityFrameworkCore;
using System;

namespace BlocketLiteEFCoreDB.DbContexts
{
    /// <summary>
    /// A Context Class that inherits from <see cref="DbContext"/>. <see cref="BlocketLiteContext"/> also seeds and implements rules for the DB.
    /// </summary>
    public class BlocketLiteContext : DbContext
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
        /// A <see cref="DbSet{TEntity}"/> of <see cref="User"/>
        /// </summary>
        public DbSet<User> Users { get; set; }

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
                modelBuilder.Entity<User>().HasData(
                    new User()
                    {
                        Id = 1,
                        UserName = "Calle",
                        Email = "Erik@gmail.com",
                        Password = "123"
                    },
                    new User()
                    {
                        Id = 2,
                        UserName = "Johan",
                        Email = "Johan@gmail.com",
                        Password = "123"
                    },
                    new User()
                    {
                        Id = 3,
                        UserName = "Alex",
                        Email = "Alex@gmail.com",
                        Password = "123"
                    },
                    new User()
                    {
                        Id = 4,
                        UserName = "Samuel",
                        Email = "Samuel@gmail.com",
                        Password = "123"
                    },
                    new User()
                    {
                        Id = 5,
                        UserName = "Jamie",
                        Email = "Jamie@gmail.com",
                        Password = "123"
                    },
                    new User()
                    {
                        Id = 6,
                        UserName = "Dracula",
                        Email = "Dracula@gmail.com",
                        Password = "123"
                    });


                modelBuilder.Entity<Advertisement>().HasData(
                    new Advertisement()
                    {
                        Id = 1,
                        Title = "Very very cool apartment for rent",
                        Description = "Big apartment with 5 rooms for rent",
                        ConstructionYear = 1978,
                        UserId = 1,
                        CreatedOn = Helpers.GetCurrentDateUTC.GetDateTimeUTC(),
                        SellingPrice = null,
                        RentingPrice = 350,
                        PropertyTypeId = 1,
                        CanBeSold = false,
                        CanBeRented = true,
                        Contact = "10708 001122",
                        Address = "Varberg Adress"
                    },
                    new Advertisement()
                    {
                        Id = 2,
                        Title = "Small house in fishing village",
                        Description = "Cozy house, close to the sea, 2 bedrooms and a big garden",
                        ConstructionYear = 1921,
                        UserId = 2,
                        CreatedOn = Helpers.GetCurrentDateUTC.GetDateTimeUTC(),
                        SellingPrice = 1200000,
                        RentingPrice = null,
                        PropertyTypeId = 2,
                        CanBeSold = true,
                        CanBeRented = false,
                        Contact = "1111-33334",
                        Address = "Håstensgatan 4"
                    },
                    new Advertisement()
                    {
                        Id = 3,
                        Title = "Office Space in London",
                        Description = "Small office space 10sq in the heart of London",
                        ConstructionYear = 2012,
                        UserId = 3,
                        CreatedOn = Helpers.GetCurrentDateUTC.GetDateTimeUTC(),
                        SellingPrice = null,
                        RentingPrice = 500,
                        PropertyTypeId = 3,
                        CanBeRented = true,
                        CanBeSold = false,
                        Contact = "000-33334",
                        Address = "Hello Adress"
                    },
                    new Advertisement()
                    {
                        Id = 4,
                        Title = "Ghost Mansion in Trasylvania",
                        Description = "Big Castle in Romania,",
                        ConstructionYear = 1700,
                        UserId = 6,
                        CreatedOn = Helpers.GetCurrentDateUTC.GetDateTimeUTC(),
                        SellingPrice = 50000,
                        RentingPrice = 10,
                        PropertyTypeId = 2,
                        CanBeRented = true,
                        CanBeSold = true,
                        Contact = "11267716",
                        Address = "BlPark123"
                    });

                modelBuilder.Entity<Comment>().HasData(
                    new Comment
                    {
                        Id = 1,
                        AdvertisementId = 1,
                        Content = "First comment by Calle",
                        CreatedOn = DateTime.Now,
                        UserId = 1,
                        UserName = "Calle"
                    },
                    new Comment
                    {
                        Id = 2,
                        AdvertisementId = 2,
                        Content = "huml, humla humla. Comment 2",
                        CreatedOn = DateTime.Now,
                        UserId = 2,
                        UserName = "Johan"
                    },
                    new Comment
                    {
                        Id = 3,
                        AdvertisementId = 1,
                        Content = "Cool, what is this",
                        CreatedOn = DateTime.Now,
                        UserId = 3,
                        UserName = "Alex"
                    },
                    new Comment
                    {
                        Id = 4,
                        AdvertisementId = 1,
                        Content = "What is wrong with this comment",
                        CreatedOn = DateTime.Now.AddDays(-1),
                        UserId = 1,
                        UserName = "Calle"
                    },
                    new Comment
                    {
                        Id = 5,
                        AdvertisementId = 4,
                        Content = "What is this? Really cheap",
                        CreatedOn = DateTime.Now,
                        UserId = 5,
                        UserName = "Jamie"
                    },
                    new Comment
                    {
                        Id = 6,
                        AdvertisementId = 4,
                        Content = "Yes, i need new host in the castle",
                        CreatedOn = DateTime.Now,
                        UserId = 6,
                        UserName = "Dracula"
                    });               
        
                modelBuilder.Entity<Rating>().HasData(
                    new Rating
                    {
                        Id = 1,
                        Value = 3,
                        RatedUserId = 1,
                        RatingUserId = 2
                    },
                    new Rating
                    {
                        Id = 2,
                        Value = 2,
                        RatedUserId = 1,
                        RatingUserId = 3
                    },
                    new Rating
                    {
                        Id = 3,
                        Value = 5,
                        RatedUserId = 5,
                        RatingUserId = 6
                    });
            }       

            base.OnModelCreating(modelBuilder);
        }
    }
}
