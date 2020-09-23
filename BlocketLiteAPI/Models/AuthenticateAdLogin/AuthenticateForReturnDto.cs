using BlocketLiteEFCoreDB.Entities;
using System;

namespace BlocketLiteAPI.Models
{
    /// <summary>
    /// A model to return after an <see cref="User"/> have logged-in.
    /// </summary>
    public class AuthenticateForReturnDto
    {
        /// <summary>
        /// A decrypted JWToken string
        /// </summary>
        public string Access_Token { get; set; }

        /// <summary>
        /// The token type of the token.
        /// </summary>
        public string Token_type { get; set; } = "Bearer";

        /// <summary>
        /// //TODO
        /// </summary>
        public int Expires_In { get; set; }

        /// <summary>
        /// The user name of the current <see cref="User"/>
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// The <see cref="DateTimeOffset"/> when the token is issued.
        /// </summary>
        public DateTimeOffset Issued { get; set; }

        /// <summary>
        /// The <see cref="DateTimeOffset"/> whe the token is to expire.
        /// </summary>
        public DateTimeOffset Expires { get; set; }
    }
}
