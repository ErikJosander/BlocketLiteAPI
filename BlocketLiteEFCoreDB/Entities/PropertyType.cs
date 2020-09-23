using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlocketLiteEFCoreDB.Entities
{
    public class PropertyType
    {
        /// <summary>
        /// Unique id-number 
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        /// <summary>
        /// A collection of PropertyTypes - (1:apartment, 2:house, 3:office, 4:warehouse)
        /// </summary>
        [Required]
        [MaxLength(10)]
        public string Type { get; set; }

        public ICollection<Advertisement> Advertisments { get; set; } = new List<Advertisement>();
    }
}
