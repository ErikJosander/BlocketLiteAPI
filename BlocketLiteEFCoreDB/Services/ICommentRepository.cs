using BlocketLiteEFCoreDB.Entities;
using BlocketLiteEFCoreDB.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlocketLiteEFCoreDB.Services
{
    /// <summary>
    /// Interface that defins contracts for the <see cref="CommentRepository"/>
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
        Task<ICollection<Comment>> GetAllFromRealEstateAsync(int realEstateId, int skip, int take);

        /// <summary>
        /// If <see cref="DbContexts.BlocketLiteContext.Advertisements"/><see cref="Advertisement.Id"/> is equal to <paramref name="realEstateId"/>"/>
        /// <br></br> returns <see cref="ICollection{T}"/> of <see cref="Comment"/>.
        /// </summary>
        /// <param name="realEstateId"></param>    
        /// <returns>An <see cref="ICollection{T}"/> of <see cref="Comment"/></returns>
        Task<ICollection<Comment>> GetAllFromRealEstateAsync(int realEstateId);

        /// <summary>
        /// Return an <see cref="ICollection{T}"/> of all <see cref="Comment"/> who are posted by a specific <see cref="User"/>
        /// <br></br> Optional <paramref name="skip"/> and <paramref name="take"/> parameters.
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="skip"></param>
        /// <param name="take"></param>
        /// <returns><see cref="ICollection{T} <see cref="Comment"/>"/></returns>
        Task<ICollection<Comment>> GetAllFromUserAsync(string userName, int skip, int take);

        /// <summary>
        /// If <see cref=DbContexts.BlocketLiteContext.Comments"/> contains an <see cref="Comment"/> with <see cref="Comment.UserName"/> 
        /// <br></br> that is equal to <paramref name="userName"/> return <see cref="Comment.UserId"/>.
        /// </summary>
        /// <param name="userName"></param>
        /// <returns><see cref="Comment.UserId"/></returns>
        string GetUserIdFromUserName(string userName);

        /// <summary>
        /// Returns an <see cref="int"/> count of all the <see cref="Comment"/> that have t he same <see cref="Comment.UserId"/>
        /// </summary>
        /// <param name="userId"></param>
        /// <returns><see cref="int"/></returns>
        int GetNumberOfComments(string userId);
    }
}
