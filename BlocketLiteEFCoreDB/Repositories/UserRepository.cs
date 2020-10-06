using BlocketLiteEFCoreDB.DbContexts;
using BlocketLiteEFCoreDB.Entities;
using BlocketLiteEFCoreDB.Services;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BlocketLiteEFCoreDB.Repositories
{
    /// <summary>
    /// Repository that implements <see cref="IUserRepository"/>
    /// </summary>
    public class UserRepository : Repository<User>, IUserRepository
    {
        private readonly BlocketLiteContext _context;

        // Constructor
        public UserRepository(BlocketLiteContext context)
            : base(context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        /// <summary>
        /// Return all the <see cref="IEnumerable{T}"/> of <see cref="Advertisement"/> in the <see cref="DbContexts.BlocketLiteContext.Advertisements"/>
        /// <br></br> where <see cref="Advertisement.UserId"/> is equal to <paramref name="userId"/>.
        /// </summary>
        /// <param name="userId"></param>
        /// <returns><see cref="IEnumerable{T}"/> of <see cref="Advertisement"/></returns>
        public IEnumerable<Advertisement> GetAdvertisements(string userId)
        {
            return _context.Advertisements.Where(ui => ui.UserId == userId);
        }

        /// <summary>
        /// Returns <see cref="IEnumerable{T}"/> of <see cref="User"/> in the <see cref="DbContexts.BlocketLiteContext.Users"/> table in the DB.
        /// </summary>
        /// <param name="userName"></param>
        /// <returns><see cref="IEnumerable{T}"/> of <see cref="User"/></returns>
        public IEnumerable<User> GetAll(string userName)
        {
            if (string.IsNullOrWhiteSpace(userName))
            {
                return GetAll();
            }

            var collection = _context.Users as IQueryable<User>;

            if (!string.IsNullOrWhiteSpace(userName))
            {
                userName = userName.Trim();
                collection = collection.Where(a => a.UserName.Contains(userName));
            }

            return collection.ToList();
        }

        /// <summary>
        /// If <see cref="DbContexts.BlocketLiteContext.Users"/> contains a <see cref="User"/> with <see cref="User.UserName"/> that is equal to <paramref name="userName"/>.
        /// <br></br> return it.
        /// </summary>
        /// <param name="userName"></param>
        /// <returns><see cref="User"/></returns>
        public User GetFromUserName(string userName)
        {
            return _context.Users.Where(u => u.UserName == userName).FirstOrDefault();
        }
    }
}
