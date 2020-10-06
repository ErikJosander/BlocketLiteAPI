using BlocketLiteAPI.ValidationAttributes;
using System;
using System.ComponentModel.DataAnnotations;

namespace BlocketLiteAPI.Models
{
    /// <summary>
    /// A model class for creating a <see cref="Advertisment"/>.
    /// </summary>
    [AdvertismentHeadingMustBeDifferentFromDescription]
    [ConstructionYearMustBeLesserOrEqualToThisYear]
    [SellingAndRentalPriceCantBothBeNull]
    public class AdvertisementForCreationDto
    {

        /// <summary>
        /// Title of the advertisment.
        /// </summary>
        [Required(ErrorMessage = "You need to fill out a heading.")]
        [MinLength(5, ErrorMessage = "The heading should have more than 5 character.")]
        [MaxLength(50, ErrorMessage = "The heading should not have more than 50 characters.")]
        public string Title { get; set; }

        /// <summary>
        /// The description of the advertsiment.
        /// </summary>
        [Required(ErrorMessage = "You need to fill out a description")]
        [MinLength(10, ErrorMessage = "The description should have more than 10 characters")]
        [MaxLength(500, ErrorMessage = "The description should not have more than 500 characters")]
        public string Description { get; set; }

        /// <summary>
        /// The year-of-build of the advertismet. Needs to be an <see cref="int"/> of only 4 digits.
        /// </summary>
        [Required(ErrorMessage = "You need to fill out a year-of-build")]
        [Range(1600, 2500, ErrorMessage = "The constructionyear need to be between 1600 and the current year")]
        public int ConstructionYear { get; set; }

        /// <summary>
        /// The Selling-Price, can be <see cref="Nullable"/> (can't be <see cref="Nullable"/> if the <see cref="RentingPrice"/> is <see cref="Nullable"/>).
        /// </summary>
        [Range(1, 500000000)]
        public int? SellingPrice { get; set; }

        /// <summary>
        /// The renting price, can be <see cref="Nullable"/> (can't be <see cref="Nullable"/> if the <see cref="SellingPrice"/> is <see cref="Nullable"/>).
        /// </summary>
        [Range(1, 50000000)]
        public int? RentingPrice { get; set; }

        /// <summary>
        /// Connects to the <see cref="Properties"/> table in the DB.
        /// <br></br>1:apartment, 2:house, 3:office, 4:werehouse.
        /// </summary>
        [Required(ErrorMessage = "You need to fill out a property-type-id.")]
        [Range(1, 4)]
        //public int PropertyTypeId { get; set; }
        public int Type { get; set; }

        /// <summary>
        /// A link to a picture
        /// </summary>
        public string ImageUrl { get; set; }

        /// <summary>
        /// The address of the property.
        /// </summary>
        [Required(ErrorMessage = "You need to fill out the adress.")]
        [MinLength(2), MaxLength(100)]
        public string Address { get; set; }

        /// <summary>
        /// The contact number to the seller/renter.
        /// </summary>
        [MinLength(8), MaxLength(15)]
        [Required(ErrorMessage = "You need to fill the contact number")]
        public string Contact { get; set; }
    }
}
