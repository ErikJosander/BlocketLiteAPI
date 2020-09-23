using BlocketLiteEFCoreDB.DbContexts;
using BlocketLiteEFCoreDB.Entities;
using BlocketLiteEFCoreDB.Services;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BlocketLiteEFCoreDB.Repositories
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        private readonly BlocketLiteContext _context;

        public UserRepository(BlocketLiteContext context)
            : base(context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public IEnumerable<Advertisement> GetAdvertisements(int userId)
        {
            return _context.Advertisements.Where(ui => ui.UserId == userId);
        }

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

        public User GetFromUserName(string userName)
        {
            return _context.Users.Where(u => u.UserName == userName).FirstOrDefault();
        }

        IEnumerable<User> IUserRepository.GetPassword()
        {
            throw new NotImplementedException();
        }
    }
}
