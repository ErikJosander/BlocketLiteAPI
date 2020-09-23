using BlocketLiteEFCoreDB.Entities;
using System.Collections.Generic;
using BlocketLiteEFCoreDB.Repositories;

namespace BlocketLiteEFCoreDB.Services
{
    /// <summary>
    /// Interface that defins contracts for the <see cref="ICommentRepository"/>
    /// </summary>
    public interface ICommentRepository : IRepository<Comment>
    {
        /// <summary>
        /// If <see cref="DbContexts.BlocketLiteContext.Advertisements"/><see cref="Advertisement.Id"/> is equal to <paramref name="realEstateId"/>"/>
        /// <br></br> returns <see cref="ICollection{T}"/> of <see cref="Comment"/>.
        /// <br></br> Optional <paramref name="skip"/> and <paramref name="take"/> parameters.
        /// </summary>
        /// <param name="realEstateId"></param>
        /// <param name="skip"></param>
        /// <param name="take"></param>
        /// <returns>An <see cref="ICollection{T}"/> of <see cref="Comment"/></returns>
        ICollection<Comment> GetAllFromRealEstate(int realEstateId, int skip, int take);

        /// <summary>
        /// Return an <see cref="ICollection{T}"/> of all <see cref="Comment"/> who are posted by a specific <see cref="User"/>
        /// <br></br> Optional <paramref name="skip"/> and <paramref name="take"/> parameters.
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="skip"></param>
        /// <param name="take"></param>
        /// <returns><see cref="ICollection{T} <see cref="Comment"/>"/></returns>
        ICollection<Comment> GetAllUser(string userName, int skip, int take);

        /// <summary>
        /// If <see cref=DbContexts.BlocketLiteContext.Comments"/> contains an <see cref="Comment"/> with <see cref="Comment.UserName"/> 
        /// <br></br> that is equal to <paramref name="userName"/> return <see cref="Comment.UserId"/>.
        /// </summary>
        /// <param name="userName"></param>
        /// <returns><see cref="Comment.UserId"/></returns>
        int? GetUserIdFromUserName(string userName);


        int GetNumberOfComments(int userId);
    }
}
