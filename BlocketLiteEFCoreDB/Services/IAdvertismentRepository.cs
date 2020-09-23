using BlocketLiteEFCoreDB.Entities;
using System.Collections.Generic;

namespace BlocketLiteEFCoreDB.Services
{
    public interface IAdvertismentRepository : IRepository<Advertisement>
    {
        IEnumerable<Advertisement> GetAll(int skip, int take);
        string GetPropertyNameFromPropertyId(int id);
        string GetUserNameFromUserId(int id);
        ICollection<Comment> GetComments(int id);
        int GetUserIdFromUserName(string userName);
        int GetNumberOfProperties(int userId);
    }
}
