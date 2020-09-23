using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlocketLiteEFCoreDB.Entities
{
    /// <summary>
    /// Public <see cref="Entities"/> that is stored in the DB.
    /// </summary>
    public class PropertyType
    {
        /// <summary>
        /// Unique id-number 
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        /// <summary>
        /// A string of <see cref="PropertyType"/> - (1:apartment, 2:house, 3:office, 4:warehouse)
        /// </summary>
        [Required]
        [MaxLength(10)]
        public string Type { get; set; }

        /// <summary>
        /// A <see cref="ICollection{T}"/> of > <see cref="Advertisement"/>
        /// </summary>
        public ICollection<Advertisement> Advertisments { get; set; } = new List<Advertisement>();
    }
}
