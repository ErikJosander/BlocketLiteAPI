using BlocketLiteEFCoreDB.DbContexts;
using BlocketLiteEFCoreDB.Entities;
using BlocketLiteEFCoreDB.Services;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BlocketLiteEFCoreDB.Repositories
{
    /// <summary>
    /// Repository that implements <see cref="ICommentRepository"/>
    /// </summary>
    public class CommentRepository : Repository<Comment>, ICommentRepository
    {
        private readonly BlocketLiteContext _context;
        
        // Constructor
        public CommentRepository(BlocketLiteContext context) : base(context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        /// <summary>
        /// If <see cref="DbContexts.BlocketLiteContext.Advertisements"/><see cref="Advertisement.Id"/> is equal to <paramref name="realEstateId"/>"/>
        /// <br></br> returns <see cref="ICollection{T}"/> of <see cref="Comment"/>.
        /// <br></br> Optional <paramref name="skip"/> and <paramref name="take"/> parameters.
        /// </summary>
        /// <param name="realEstateId"></param>
        /// <param name="skip"></param>
        /// <param name="take"></param>
        /// <returns>An <see cref="ICollection{T}"/> of <see cref="Comment"/></returns>
        public ICollection<Comment> GetAllFromRealEstate(int realEstateId, int skip, int take)
        {
            var collection = _context.Comments.Where(c => c.AdvertisementId == realEstateId).OrderBy(c => c.CreatedOn).ToList();
            if (collection == null)
            {
                return null;
            }
            if (take > 100) take = 100;
            //if (take < 10) take = 10; ? User should have opportunity to  take less than 10...
            //if (skip < 0) skip = 0; Makes the validation in the controller..
            // if skip is bigger than the collection => return 0, not -1.
            //if (skip > collection.Count()) skip = (collection.Count()-1);
            if (skip > collection.Count()) skip = collection.Count();

            List<Comment> collectionOutput = new List<Comment>();
            try
            {
                collectionOutput = collection.ToList().GetRange(skip, take);

            }
            catch (ArgumentException)
            {
                take = collection.Count() - skip;
                collectionOutput = collection.ToList().GetRange(skip, take);
            }

            return collectionOutput;
        }

        /// <summary>
        /// Return an <see cref="ICollection{T}"/> of all <see cref="Comment"/> who are posted by a specific <see cref="User"/>
        /// <br></br> Optional <paramref name="skip"/> and <paramref name="take"/> parameters.
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="skip"></param>
        /// <param name="take"></param>
        /// <returns><see cref="ICollection{T} <see cref="Comment"/>"/></returns>
        public ICollection<Comment> GetAllFromUser(string userName, int skip, int take)
        {
            var collection = _context.Comments.Where(c => c.UserName == userName).OrderBy(c => c.CreatedOn).ToList();
            if (collection == null)
            {
                return null;
            }
            if (take > 100) take = 100;
            //if (take < 10) take = 10; ? User should have opportunity to  take less than 10...
            //if (take < 10) take = 10; 
            //if (skip < 0) skip = 0; Makes the validation in the controller..
            if (skip > collection.Count()) skip = collection.Count();

            List<Comment> collectionOutput = new List<Comment>();
            try
            {
                collectionOutput = collection.ToList().GetRange(skip, take);

            }
            catch (ArgumentException)
            {
                take = collection.Count() - skip;
                collectionOutput = collection.ToList().GetRange(skip, take);
            }

            return collectionOutput;
        }

        /// <summary>
        /// Returns an <see cref="int"/> count of all the <see cref="Comment"/> that have t he same <see cref="Comment.UserId"/>
        /// </summary>
        /// <param name="userId"></param>
        /// <returns><see cref="int"/></returns>
        public int GetNumberOfComments(int userId)
        {
            var collection = _context.Comments.Where(c => c.UserId == userId).ToList();
            return collection.Count();
        }

        /// <summary>
        /// If <see cref=DbContexts.BlocketLiteContext.Comments"/> contains an <see cref="Comment"/> with <see cref="Comment.UserName"/> 
        /// <br></br> that is equal to <paramref name="userName"/> return <see cref="Comment.UserId"/>.
        /// </summary>
        /// <param name="userName"></param>
        /// <returns><see cref="Comment.UserId"/></returns>
        public int? GetUserIdFromUserName(string userName)
        {
            var user = _context.Users.Where(u => u.UserName == userName).FirstOrDefault();
            return user.Id;
        }
    }
}
