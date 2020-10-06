using BlocketLiteEFCoreDB.DbValidationAttributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlocketLiteEFCoreDB.Entities
{
    /// <summary>
    /// Public <see cref="Entities"/> that is stored in the DB.
    /// </summary>
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
        /// A link to a picture
        /// </summary>
        public string ImageUrl { get; set; }

        /// <summary>
        /// A description of the advertisment
        /// </summary>
        [Required]
        [MinLength(10), MaxLength(1000)]
        public string Description { get; set; }

        /// <summary>
        /// The year-of-build. Needs to be 4-digits.
        /// </summary>
        [Required]
        [MaxLength(4)]
        [Range(1600, 2500)]
        public int ConstructionYear { get; set; }

        /// <summary>
        /// A <see cref="DateTimeOffset"/> whne the advertisment is created.
        /// </summary>
        [Required]
        public DateTimeOffset CreatedOn { get; set; }

        /// <summary>
        /// The Selling-Price, can be <see cref="Nullable"/> (can't be <see cref="Nullable"/> if the <see cref="RentingPrice"/> is <see cref="Nullable"/>).
        /// </summary>
        [MinLength(2), MaxLength(50)]
        public int? SellingPrice { get; set; }

        /// <summary>
        /// The renting price, can be <see cref="Nullable"/> (can't be <see cref="Nullable"/> if the <see cref="SellingPrice"/> is <see cref="Nullable"/>).
        /// </summary>
        [MinLength(2), MaxLength(50)]
        public int? RentingPrice { get; set; }

        /// <summary>
        /// The <see cref="ForeignKeyAttribute"/> for a specific <see cref="User"/>
        /// </summary>
        [ForeignKey("UserId")]
        public virtual User User { get; set; }
        public string UserId { get; set; }

        /// <summary>
        /// The <see cref="ForeignKeyAttribute"/> for a specific <see cref="PropertyType"/>
        /// </summary>
        [ForeignKey("PropertyTypeId")]
        public PropertyType PropertyType { get; set; }
        public int PropertyTypeId { get; set; }

        /// <summary>
        /// <see cref="Boolean"/> True if the propert can be sold. If <see cref="CanBeRented"/> is null. <see cref="CanBeSold"/> can't be null.
        /// </summary>
        [Required]
        public bool CanBeSold { get; set; }

        /// <summary>
        /// <see cref="Boolean"/> True if the property can be rented. If <see cref="CanBeSold"/> is null. <see cref="CanBeRented"/> can't be null.
        /// </summary>
        [Required]
        public bool CanBeRented { get; set; }

        /// <summary>
        /// The correct adrress to the property.
        /// </summary>
        [Required]
        public string Address { get; set; }

        /// <summary>
        /// The contact number to the seller.
        /// </summary>
        [MinLength(8), MaxLength(15)]
        [Required]
        public string Contact { get; set; }

        /// <summary>
        /// A <see cref="Nullable"/> <see cref="ICollection{T}"/> of <see cref="Comment"/>. 
        /// </summary>
        public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();
    }
}
