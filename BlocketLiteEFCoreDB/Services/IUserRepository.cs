using BlocketLiteEFCoreDB.Entities;
using System.Collections.Generic;

namespace BlocketLiteEFCoreDB.Services
{
    public interface IUserRepository : IRepository<User>
    {
        IEnumerable<User> GetPassword();
        IEnumerable<User> GetAll(string userName);
        IEnumerable<Advertisement> GetAdvertisements(int userId);
        User GetFromUserName(string userName);
    }
}
