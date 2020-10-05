using BlocketLiteEFCoreDB.DbContexts;
using BlocketLiteEFCoreDB.Entities;
using BlocketLiteEFCoreDB.Services;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BlocketLiteEFCoreDB.Repositories
{
    public class AdvertismentRepository : Repository<Advertisement>, IAdvertismentRepository
    {
        private readonly BlocketLiteContext _context;

        public AdvertismentRepository(BlocketLiteContext context) : base(context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public IEnumerable<Advertisement> GetAll(int skip, int take)
        {
            var collection = _context.Advertisements as IQueryable<Advertisement>;
            if (take == 0) take = collection.Count();
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

        public string GetPropertyNameFromPropertyId(int propertyId)
        {
            var propertyType = _context.PropertyTypes.Where(pt => pt.Id == propertyId).FirstOrDefault();
            return propertyType.Type;
        }

        public string GetUserNameFromUserId(int userId)
        {
            var user = _context.Users.Where(pt => pt.Id == userId).FirstOrDefault();
            return user.UserName;
        }

        public ICollection<Comment> GetComments(int id)
        {
            return _context.Comments.Where(c => c.AdvertisementId == id).ToList();
        }

        public int GetUserIdFromUserName(string userName)
        {
            var user = _context.Users.Where(u => u.UserName == userName).FirstOrDefault();
            return user.Id;
        }

        public int GetNumberOfProperties(int userId)
        {
            var collection = _context.Advertisements.Where(c => c.UserId == userId).ToList();
            return collection.Count();
        }
    }
}
