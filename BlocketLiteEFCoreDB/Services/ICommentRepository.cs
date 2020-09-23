using BlocketLiteEFCoreDB.Entities;
using System.Collections.Generic;

namespace BlocketLiteEFCoreDB.Services
{
    public interface ICommentRepository : IRepository<Comment>
    {
        ICollection<Comment> GetAllFromRealEstate(int realEstateId, int skip, int take);
        ICollection<Comment> GetAllUser(string userName, int skip, int take);
        int? GetUserIdFromUserName(string userName);
        int GetNumberOfComments(int userId);
    }
}
