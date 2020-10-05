using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlocketLiteEFCoreDB.Entities
{
    /// <summary>
    /// Public <see cref="Entities"/> that is stored in the DB.
    /// </summary>
    public class User : IdentityUser
    { 
        /// <summary>
        /// The users username.. duh
        /// </summary>
        [Required]
        [MaxLength(50)]
        public override string UserName { get; set; }


        /// <summary>
        /// The Email-address to the user.
        /// </summary>
        [Required]
        [EmailAddress]
        [MaxLength(50)]
        public override string Email { get; set; }


        /// <summary>
        /// Password for the user
        /// </summary>
        [Required]
        [PasswordPropertyText]
        [MinLength(3), MaxLength(32)]
        public string Password { get; set; }


        /// <summary>
        /// A <see cref="ICollection{T}"/> of <see cref="Rating"/>.
        /// </summary>
        public virtual ICollection<Rating> Ratings { get; set; } = new List<Rating>();
    }
}
