using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlocketLiteEFCoreDB.Entities
{
    /// <summary>
    /// Public <see cref="Entities"/> that is stored in the DB.
    /// </summary>
    public class Rating
    {
        /// <summary>
        /// Unique identifier
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// A value (<see cref="int"/>) rating from 1-5 an <see cref="User"/> can place on another <see cref="User"/>.
        /// </summary>
        [Range(1, 5)]
        [Required]
        public int Value { get; set; }


        /// <summary>
        /// <see cref="ForeignKeyAttribute"/> to the rated <see cref="User"/>.
        /// </summary>
        [ForeignKey("RatedUserId")]
        public virtual User RatedUser { get; set; }
        public int? RatedUserId { get; set; }


        /// <summary>
        /// <see cref="ForeignKeyAttribute"/> to the rating <see cref="User"/>.
        /// </summary>      
        [ForeignKey("RatingUserId")]
        public virtual User RatingUser { get; set; }
        public int? RatingUserId { get; set; }
    }
}