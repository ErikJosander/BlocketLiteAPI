using BlocketLiteEFCoreDB.DbContexts;
using BlocketLiteEFCoreDB.Entities;
using BlocketLiteEFCoreDB.Services;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BlocketLiteEFCoreDB.Repositories
{
    /// <summary>
    /// Repository that implements <see cref="IAdvertismentRepository"/>
    /// </summary>
    public class AdvertismentRepository : Repository<Advertisement>, IAdvertismentRepository
    {
        private readonly BlocketLiteContext _context;

        // Constructor
        public AdvertismentRepository(BlocketLiteContext context) : base(context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }


        /// <summary>
        /// Gets all the <see cref="Advertisement"/> from the DB. Optional <paramref name="skip"/> and <paramref name="take"/> parameters.
        /// </summary>
        /// <param name="skip"></param>
        /// <param name="take"></param>
        /// <returns>An <see cref="IEnumerable{T}"/> of <see cref="Advertisement"/>.</returns>
        public IEnumerable<Advertisement> GetAll(int skip, int take)
        {
            var collection = _context.Advertisements as IQueryable<Advertisement>;
            if (take == 0) take = collection.Count();
<<<<<<< HEAD
=======
            if (take < 10) take = 10;
>>>>>>> 6cdbe1daac461b98a0510ae3813c369f3469857d
            if (skip < 0) skip = 0;
            if (skip > collection.Count()) skip = (collection.Count() - 1);


            List<Advertisement> collectionOutput = new List<Advertisement>();
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
        /// If <see cref="PropertyType.Id"/> is equal to <paramref name="id"/> return <see cref="PropertyType.Type"/>
        /// </summary>
        /// <param name="id"></param>
        /// <returns><see cref="PropertyType.Type"/></returns>
        public string GetPropertyNameFromPropertyId(int propertyId)
        {
            var propertyType = _context.PropertyTypes.Where(pt => pt.Id == propertyId).FirstOrDefault();
            return propertyType.Type;
        }

        /// <summary>
        /// If <see cref="User.Id"/> is equal to <paramref name="id"/> return <see cref="User.UserName"/>
        /// </summary>
        /// <param name="id"></param>
        /// <returns><see cref="User.UserName"/></returns>
        public string GetUserNameFromUserId(int userId)
        {
            var user = _context.Users.Where(pt => pt.Id == userId).FirstOrDefault();
            return user.UserName;
        }

        /// <summary>
        /// If <paramref name="id"/> is equal to <see cref="Advertisement.Id"/> return <see cref="Advertisement.Comments"/>
        /// </summary>
        /// <param name="id"></param>
        /// <returns><see cref="Advertisement.Comments"/></returns>
        public ICollection<Comment> GetComments(int id)
        {
            return _context.Comments.Where(c => c.AdvertisementId == id).ToList();
        }

        /// <summary>
        /// If <see cref=DbContexts.BlocketLiteContext.Users"/> contains an <see cref="User"/> with <see cref="User.UserName"/> 
        /// <br></br> that is equal to <paramref name="userName"/> return <see cref="User.Id"/>.
        /// </summary>
        /// <param name="userName"></param>
        /// <returns><see cref="User.Id"/></returns>
        public int GetUserIdFromUserName(string userName)
        {
            var user = _context.Users.Where(u => u.UserName == userName).FirstOrDefault();
            return user.Id;
        }

        /// <summary>
        /// Returns the the number of <see cref="Advertisement"/> connected to a single <see cref="User"/>
        /// <br></br> if the <paramref name="userId"/> is foud in the <see cref="DbContexts.BlocketLiteContext.Advertisements"/><see cref="Advertisement.UserId"/>
        /// </summary>
        /// <param name="userId"></param>
        /// <returns>Number of <see cref="Advertisement"/> <see cref="int"/></returns>
        public int GetNumberOfProperties(int userId)
        {
            var collection = _context.Advertisements.Where(c => c.UserId == userId).ToList();
            return collection.Count();
        }
    }
}
