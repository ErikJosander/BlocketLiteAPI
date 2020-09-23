using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlocketLiteEFCoreDB.Entities
{
    public class User
    {
        /// <summary>
        /// Unique id-number 
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        /// <summary>
        /// The users username.. duh
        /// </summary>
        [Required]
        [MaxLength(50)]
        public string UserName { get; set; }


        /// <summary>
        /// Email to the user
        /// </summary>
        [Required]
        [EmailAddress]
        [MaxLength(50)]
        public string Email { get; set; }


        /// <summary>
        /// Password for the user
        /// </summary>
        [Required]
        [PasswordPropertyText]
        [MinLength(3), MaxLength(32)]
        public string Password { get; set; }

        public virtual ICollection<Rating> Ratings { get; set; } = new List<Rating>();
    }
}
