using BlocketLiteEFCoreDB.Entities;
using System;
using System.ComponentModel.DataAnnotations;

namespace BlocketLiteAPI.Models
{
    /// <summary>
    /// A model for mapping <see cref="RatingForCreationDto"/> to an <see cref="Rating"/>.    
    /// /// </summary>
    public class RatingForCreationDto
    {
        /// <summary>
        /// The Value of the <see cref="Rating"/>.
        /// </summary>
        [Required]
        [Range(1, 5)]
        public int Value { get; set; }


        /// <summary>
        /// The <see cref="User"/> of the <see cref="User"/> who is rated.
        /// </summary>
        [Required]
        public string UserId { get; set; }
    }
}
