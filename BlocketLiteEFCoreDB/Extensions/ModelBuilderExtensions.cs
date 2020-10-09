using BlocketLiteEFCoreDB.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlocketLiteEFCoreDB.Extensions
{
    public static class ModelBuilderExtensions
    {
        /// <summary>
        /// Creates data for development purposes if <see cref="Environment"/> is set to Development
        /// <br></br>
        /// Provides several entities of <see cref="User"/>, <see cref="Advertisement"/>, <see cref="Comment"/>, <see cref="Rating"/>.
        /// <br></br>
        /// OBS: Does not provide seed data on Production.
        /// </summary>
        /// <param name="modelBuilder"></param>
        public static void CreateDevSeedData(this ModelBuilder modelBuilder)
        {
            var environment = (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT"));
            if (environment == "Development")
            {
                List<User> users = new List<User>();
                modelBuilder.Entity<IdentityRole>().HasData(
                   new IdentityRole { Name = "User", NormalizedName = "USER" });

                // Create the user Erik
                User user = new User
                {
                    UserName = "Erik",
                    Password = "123",
                    LockoutEnabled = true,
                    Email = "Erik@test.com",
                    NormalizedEmail = "Erik@test.com".ToUpper(),
                    NormalizedUserName = "Erik".ToUpper(),
                    TwoFactorEnabled = false,
                    EmailConfirmed = true,
                    PhoneNumber = "123456789",
                    PhoneNumberConfirmed = false
                };
                PasswordHasher<User> ph1 = new PasswordHasher<User>();
                user.PasswordHash = ph1.HashPassword(user, "123");
                users.Add(user);
                modelBuilder.Entity<User>().HasData(user);


                // Create the user Calle
                user = new User
                {
                    UserName = "Calle",
                    LockoutEnabled = true,
                    Email = "Calle@test.com",
                    Password = "123",
                    NormalizedEmail = "Calle@test.com".ToUpper(),
                    NormalizedUserName = "Calle".ToUpper(),
                    TwoFactorEnabled = false,
                    EmailConfirmed = true,
                    PhoneNumber = "123456789",
                    PhoneNumberConfirmed = false
                };
                user.PasswordHash = ph1.HashPassword(user, "123");
                users.Add(user);
                modelBuilder.Entity<User>().HasData(user);



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
                    },
                     new Advertisement()
                     {
                         Id = 2,
                         Title = "Seaside house",
                         Description = "Cool house with seaview for sale",
                         ConstructionYear = 1679,
                         UserId = users.Where(u => u.UserName == "Erik").FirstOrDefault().Id,
                         CreatedOn = Helpers.GetCurrentDateUTC.GetDateTimeUTC(),
                         SellingPrice = 5500000,
                         RentingPrice = null,
                         PropertyTypeId = 2,
                         CanBeSold = true,
                         CanBeRented = false,
                         Contact = "00-987-298",
                         Address = "Laholm"
                     });


                modelBuilder.Entity<Comment>().HasData(
                    new Comment
                    {
                        Id = 1,
                        AdvertisementId = 1,
                        Content = "Wow, this is amazing",
                        CreatedOn = DateTime.Now,
                        UserId = users.Where(u => u.UserName == "Calle").FirstOrDefault().Id,
                        UserName = "Calle"
                    },
                    new Comment
                    {
                        Id = 2,
                        AdvertisementId = 1,
                        Content = "how many rooms?",
                        CreatedOn = DateTime.Now,
                        UserId = users.Where(u => u.UserName == "Erik").FirstOrDefault().Id,
                        UserName = "Erik"
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
        }

        /// <summary>
        /// Creates required <see cref="PropertyType"/> Data for database.
        /// </summary>
        /// <param name="modelBuilder"></param>
        public static void CreatePropertyTypeData(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PropertyType>().HasData(
                new PropertyType()
                {
                    Id = 1,
                    Type = "lägenhet"
                },
                new PropertyType()
                {
                    Id = 2,
                    Type = "hus"
                },
                new PropertyType()
                {
                    Id = 3,
                    Type = "kontor"
                },
                new PropertyType()
                {
                    Id = 4,
                    Type = "lagerlokal"
                });
        }
    }
}
