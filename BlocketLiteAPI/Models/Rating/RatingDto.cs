using BlocketLiteEFCoreDB.Entities;

namespace BlocketLiteAPI.Models
{
    /// <summary>
    /// A model for mapping <see cref="Rating"/> to an API.    
    /// /// </summary>
    public class RatingDto
    {
        /// <summary>
        /// The unique ID
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// The Value of the <see cref="Rating"/>.
        /// </summary>
        public int Value { get; set; }

        /// <summary>
        /// The <see cref="User.Id"/> of the <see cref="User"/> who is rated.
        /// </summary>
        public int RatedUserId { get; set; }

        /// <summary>
        /// The <see cref="User.UserName"/> of the <see cref="User"/> who is rated.
        /// </summary>
        public virtual string Username { get; set; }

        /// <summary>
        /// The <see cref="User.Id"/> of the <see cref="User"/> who is rating.
        /// </summary>
        public int RatingUserId { get; set; }
    }
}
