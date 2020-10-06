using BlocketLiteEFCoreDB.Entities;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BlocketLiteEFCoreDB.Helpers
{
    public class UserCreation
    {
        public static  User CreateUserAsync(string email, string username, string password)
        {
            User user = new User()
            {
                Email =email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = username,
                Password = password
            };
            return user;
        }
    }
}
