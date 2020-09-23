using BlocketLiteEFCoreDB.Entities;

namespace BlocketLiteEFCoreDB.Services
{
    public interface IRatingRepository : IRepository<Rating>
    {
        string GetUserNameFromUserId(int userId);
        void DeleteRatingUserId(int userId);
        void DeleteRatedUserRating(int userId);
        double? GetAvarageRating(int id);
    }
}