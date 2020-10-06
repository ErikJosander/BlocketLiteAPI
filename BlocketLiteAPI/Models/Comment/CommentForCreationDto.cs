using BlocketLiteEFCoreDB.Entities;
using System.ComponentModel.DataAnnotations;

namespace BlocketLiteAPI.Models
{
    /// <summary>
    /// A Model for mapping <see cref="CommentForCreationDto"/> to <see cref="Comment"/>.
    /// </summary>
    public class CommentForCreationDto
    {
        /// <summary>
        /// The foreign key to an <see cref="Advertisement"/>
        /// </summary>
        [Required]
        public int RealEstateId { get; set; }

        /// <summary>
        /// The content of the <see cref="Comment"/>
        /// </summary>
        [MinLength(5), MaxLength(250)]
        public string Content { get; set; }
    }
}
