using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlocketLiteEFCoreDB.Entities
{
    public class Rating
    {
        /// <summary>
        /// Unique id-number 
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// A value rating from 1-5 a user can place on another user
        /// </summary>
        [Range(1, 5)]
        [Required]
        public int Value { get; set; }


        /// <summary>
        /// ForeignKey to the User table
        /// </summary>
        [ForeignKey("RatedUserId")]
        public virtual User RatedUser { get; set; }
        public int? RatedUserId { get; set; }


        /// <summary>
        /// ForeignKey to the User table
        /// </summary>      
        [ForeignKey("RatingUserId")]
        public virtual User RatingUser { get; set; }
        public int? RatingUserId { get; set; }
    }
}