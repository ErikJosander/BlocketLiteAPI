using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlocketLiteEFCoreDB.Entities
{
    /// <summary>
    /// Public <see cref="Entities"/> that is stored in the DB.
    /// </summary>
    public class Comment
    {
        /// <summary>
        /// Unique identifier 
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        /// <summary>
        /// The content of the <see cref="Comment"/>
        /// </summary
        [MinLength(5), MaxLength(250)]
        public string Content { get; set; }

        /// <summary>
        /// A <see cref="DateTime"/> when the <see cref="Comment"/> is created.
        /// </summary>
        [Required]
        public DateTime CreatedOn { get; set; }


        /// <summary>
        /// The <see cref="ForeignKeyAttribute"/> to the <see cref="Advertisement.Advertisement"/>
        /// </summary>
        [Required]
        [ForeignKey("AdvertisementId")]
        public Advertisement Advertisement { get; set; }
        public int AdvertisementId { get; set; }

        /// <summary>
        /// The <see cref="ForeignKeyAttribute"/> to the <see cref="User.User"/>
        /// </summary>
        [ForeignKey("UserId")]
        public virtual User User { get; set; }
        public string UserId { get; set; }

        /// <summary>
        /// The username of the <see cref="User"/>
        /// </summary>
        [Required]
        public string UserName { get; set; }
    }
}