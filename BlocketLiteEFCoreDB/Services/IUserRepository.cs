using BlocketLiteEFCoreDB.Entities;
using BlocketLiteEFCoreDB.Repositories;
using System.Collections.Generic;

namespace BlocketLiteEFCoreDB.Services
{
    /// <summary>
    /// Interface that defines contracts for the <see cref="UserRepository"/>
    /// </summary>
    public interface IUserRepository : IRepository<User>
    {
        /// <summary>
        /// Returns <see cref="IEnumerable{T}"/> of <see cref="User"/> in the <see cref="DbContexts.BlocketLiteContext.Users"/> table in the DB.
        /// </summary>
        /// <param name="userName"></param>
        /// <returns><see cref="IEnumerable{T}"/> of <see cref="User"/></returns>
        IEnumerable<User> GetAll(string userName);

        /// <summary>
        /// Return all the <see cref="IEnumerable{T}"/> of <see cref="Advertisement"/> in the <see cref="DbContexts.BlocketLiteContext.Advertisements"/>
        /// <br></br> where <see cref="Advertisement.UserId"/> is equal to <paramref name="userId"/>.
        /// </summary>
        /// <param name="userId"></param>
        /// <returns><see cref="IEnumerable{T}"/> of <see cref="Advertisement"/></returns>
        IEnumerable<Advertisement> GetAdvertisements(string userId);

        /// <summary>
        /// If <see cref="DbContexts.BlocketLiteContext.Users"/> contains a <see cref="User"/> with <see cref="User.UserName"/> that is equal to <paramref name="userName"/>.
        /// <br></br> return it.
        /// </summary>
        /// <param name="userName"></param>
        /// <returns><see cref="User"/></returns>
        User GetFromUserName(string userName);
    }
}
