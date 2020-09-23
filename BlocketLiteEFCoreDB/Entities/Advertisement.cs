using BlocketLiteEFCoreDB.DbValidationAttributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlocketLiteEFCoreDB.Entities
{
    [RentingAndSellingCanNotBothBeNull]
    public class Advertisement
    {
        /// <summary>
        /// Unique id-number 
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        /// <summary>
        /// A title at the head of the advertisment
        /// </summary>
        [Required]
        [MinLength(5), MaxLength(50)]
        public string Title { get; set; }

        /// <summary>
        /// A description of the advertisment
        /// </summary>
        [Required]
        [MinLength(10), MaxLength(1000)]
        public string Description { get; set; }


        /// <summary>
        /// TODO Might fix this later
        /// </summary>
        [Required]
        [MaxLength(4)]
        [Range(1600, 2500)]
        public int ConstructionYear { get; set; }

        [Required]
        public DateTimeOffset CreatedOn { get; set; }

        /// <summary>
        /// Can be null
        /// </summary>
        [MinLength(2), MaxLength(50)]
        public int? SellingPrice { get; set; }

        /// <summary>
        /// Can be null
        /// </summary>
        [MinLength(2), MaxLength(50)]
        public int? RentingPrice { get; set; }


        [ForeignKey("UserId")]
        public virtual User User { get; set; }
        public int? UserId { get; set; }


        public PropertyType PropertyType { get; set; }

        /// <summary>
        /// 1: Aparment
        /// 2: House
        /// 3: Office
        /// 4: Werehouse
        /// </summary>
        public int PropertyTypeId { get; set; }

        [Required]
        public bool CanBeSold { get; set; }

        [Required]
        public bool CanBeRented { get; set; }

        public string Address { get; set; }

        [MinLength(8), MaxLength(15)]
        [Required]
        public string Contact { get; set; }

        public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();
    }
}
