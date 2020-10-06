using BlocketLiteEFCoreDB.Entities;
using System;

namespace BlocketLiteAPI.Models
{
    /// <summary>
    /// A model for mapping <see cref="Comment"/> to an API.
    /// </summary>
    public class CommentDto
    {
        /// <summary>
        /// The content of the <see cref="Comment"/>.
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// The <see cref="User.UserName"/> of the <see cref="User"/> who created the <see cref="Comment"/>
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// The <see cref="DateTime"/> the <see cref="Comment"/> was created on.
        /// </summary>
        public DateTime CreatedOn { get; set; }

    }
}