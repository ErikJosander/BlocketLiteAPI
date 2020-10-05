using BlocketLiteEFCoreDB.DbContexts;
using BlocketLiteEFCoreDB.Entities;
using BlocketLiteEFCoreDB.Services;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BlocketLiteEFCoreDB.Repositories
{
    /// <summary>
    /// Reposioty that implements the <see cref="IRatingRepository"/>
    /// </summary>
    public class RatingRepository : Repository<Rating>, IRatingRepository
    {
        private readonly BlocketLiteContext _context;

        // Constructor
        public RatingRepository(BlocketLiteContext context) : base(context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        /// <summary>
        /// Sets the <see cref="Rating.RatingUserId"/> to null.
        /// </summary>
        /// <param name="userId"></param>
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

        /// <summary>
        /// Deletes the entire rating if an rated<see cref="User"/> is deleted.
        /// </summary>
        /// <param name="userId"></param>
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

        /// <summary>
        /// Returns an <see cref="User.UserName"/> from <see cref="DbContexts.BlocketLiteContext.Users"/> if <see cref="User.Id"/> is equal to <paramref name="userId"/>.
        /// </summary>
        /// <param name="userId"></param>
        /// <returns><see cref="User.UserName"/></returns>
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

        /// <summary>
        /// Returns an <see cref="double"/> avarage of all the ratings an <see cref="User"/> have.
        /// </summary>
        /// <param name="userId"></param>
        /// <returns><see cref="Nullable"/> <see cref="double"/></returns>
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
