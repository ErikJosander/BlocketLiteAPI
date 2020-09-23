using BlocketLiteEFCoreDB.DbContexts;
using BlocketLiteEFCoreDB.Entities;
using BlocketLiteEFCoreDB.Services;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BlocketLiteEFCoreDB.Repositories
{
    public class RatingRepository : Repository<Rating>, IRatingRepository
    {
        private readonly BlocketLiteContext _context;

        public RatingRepository(BlocketLiteContext context) : base(context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public void DeleteRatingUserId(int userId)
        {
            var ratingCollection = _context.Ratings.Where(r => r.RatedUserId == userId || r.RatingUserId == userId);
            foreach (var rating in ratingCollection)
            {
                if (rating.RatingUserId == userId)
                {
                    rating.RatingUser = null;
                    rating.RatingUserId = null;
                }
            }
        }

        public void DeleteRatedUserRating(int userId)
        {
            var ratingCollection = _context.Ratings.Where(r => r.RatedUserId == userId);
            {
                foreach (var rating in ratingCollection)
                {
                    _context.Ratings.Remove(rating);
                }
            }
        }

        public string GetUserNameFromUserId(int userId)
        {
            var userName = _context.Users.Where(u => u.Id == userId).FirstOrDefault();
            if (userName == null)
            {
                // TODO what is this?
                return "DELETED";
            }
            else
            {
                return userName.UserName;
            }
        }

        public double? GetAvarageRating(int userId)
        {
            var ratings = _context.Ratings.Where(r => r.RatedUserId == userId).ToList();

            List<double> listToCount = new List<double>();
            foreach (var rating in ratings)
            {
                listToCount.Add((double)rating.Value);
            }
            if (listToCount.Count == 0)
            {
                return null;
            }
            return listToCount.Average();
        }
    }
}
