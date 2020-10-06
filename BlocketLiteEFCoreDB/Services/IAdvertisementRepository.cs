using BlocketLiteEFCoreDB.Entities;
using System.Collections.Generic;
using BlocketLiteEFCoreDB.Repositories;

namespace BlocketLiteEFCoreDB.Services
{
    /// <summary>
    /// Interface that defins contracts for the <see cref="AdvertismentRepository"/>
    /// </summary>
    public interface IAdvertisementRepository : IRepository<Advertisement>
    {
        /// <summary>
        /// Gets all the <see cref="Advertisement"/> from the DB. Optional <paramref name="skip"/> and <paramref name="take"/> parameters.
        /// </summary>
        /// <param name="skip"></param>
        /// <param name="take"></param>
        /// <returns>An <see cref="IEnumerable{T}"/> of <see cref="Advertisement"/>.</returns>
        IEnumerable<Advertisement> GetAll(int skip, int take);

        /// <summary>
        /// If <see cref="PropertyType.Id"/> is equal to <paramref name="id"/> return <see cref="PropertyType.Type"/>
        /// </summary>
        /// <param name="id"></param>
        /// <returns><see cref="PropertyType.Type"/></returns>
        string GetPropertyNameFromPropertyId(int id);

        /// <summary>
        /// If <see cref="User.Id"/> is equal to <paramref name="id"/> return <see cref="User.UserName"/>
        /// </summary>
        /// <param name="id"></param>
        /// <returns><see cref="User.UserName"/></returns>
        string GetUserNameFromUserId(int id);

        /// <summary>
        /// If <paramref name="id"/> is equal to <see cref="Advertisement.Id"/> return <see cref="Advertisement.Comments"/>
        /// </summary>
        /// <param name="id"></param>
        /// <returns><see cref="Advertisement.Comments"/></returns>
        ICollection<Comment> GetComments(int id);

        /// <summary>
        /// If <see cref=DbContexts.BlocketLiteContext.Users"/> contains an <see cref="User"/> with <see cref="User.UserName"/> 
        /// <br></br> that is equal to <paramref name="userName"/> return <see cref="User.Id"/>.
        /// </summary>
        /// <param name="userName"></param>
        /// <returns><see cref="User.Id"/></returns>
        int GetUserIdFromUserName(string userName);

        /// <summary>
        /// Returns the the number of <see cref="Advertisement"/> connected to a single <see cref="User"/>
        /// <br></br> if the <paramref name="userId"/> is foud in the <see cref="DbContexts.BlocketLiteContext.Advertisements"/><see cref="Advertisement.UserId"/>
        /// </summary>
        /// <param name="userId"></param>
        /// <returns>Number of <see cref="Advertisement"/> <see cref="int"/></returns>
        int GetNumberOfProperties(int userId);
    }
}
