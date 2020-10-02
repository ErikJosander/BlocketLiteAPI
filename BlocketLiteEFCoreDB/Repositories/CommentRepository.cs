using BlocketLiteEFCoreDB.DbContexts;
using BlocketLiteEFCoreDB.Entities;
using BlocketLiteEFCoreDB.Services;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BlocketLiteEFCoreDB.Repositories
{
    public class CommentRepository : Repository<Comment>, ICommentRepository
    {
        private readonly BlocketLiteContext _context;
        public CommentRepository(BlocketLiteContext context) : base(context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

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

        public ICollection<Comment> GetAllFromUser(string userName, int skip, int take)
        {
            var collection = _context.Comments.Where(c => c.UserName == userName).ToList();
            if (collection == null)
            {
                return null;
            }
            if (take > 100) take = 100;
            if (take < 10) take = 10;
            if (skip < 0) skip = 0;
            if (skip > collection.Count()) skip = (collection.Count() - 1);

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

        public int GetNumberOfComments(int userId)
        {
            var collection = _context.Comments.Where(c => c.UserId == userId).ToList();
            return collection.Count();
        }

        public int? GetUserIdFromUserName(string userName)
        {
            var user = _context.Comments.Where(c => c.UserName == userName).FirstOrDefault();
            return user.UserId;
        }
    }
}
