using System;
using BlocketLiteEFCoreDB.Entities;
using BlocketLiteEFCoreDB.Repositories;

namespace BlocketLiteEFCoreDB.Services
{
    /// <summary>
    /// Interface that defines contracts for the <see cref="RatingRepository"/>
    /// </summary>
    public interface IRatingRepository : IRepository<Rating>
    {
        /// <summary>
        /// Returns an <see cref="User.UserName"/> from <see cref="DbContexts.BlocketLiteContext.Users"/> if <see cref="User.Id"/> is equal to <paramref name="userId"/>.
        /// </summary>
        /// <param name="userId"></param>
        /// <returns><see cref="User.UserName"/></returns>
        string GetUserNameFromUserId(int userId);

        /// <summary>
        /// Sets the <see cref="Rating.RatingUserId"/> to null.
        /// </summary>
        /// <param name="userId"></param>
        void DeleteRatingUserId(int userId);

        /// <summary>
        /// Deletes the entire rating if an rated<see cref="User"/> is deleted.
        /// </summary>
        /// <param name="userId"></param>
        void DeleteRatedUserRating(int userId);

        /// <summary>
        /// Returns an <see cref="double"/> avarage of all the ratings an <see cref="User"/> have.
        /// </summary>
        /// <param name="userId"></param>
        /// <returns><see cref="Nullable"/> <see cref="double"/></returns>
        double? GetAvarageRating(int userId);
    }
}