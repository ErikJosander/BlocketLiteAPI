using System;
using System.Security.Policy;

namespace BlocketLiteAPI.Models
{
    /// <summary>
    /// A model class for mapping <see cref="Advertisment"/> to an XML-string
    /// </summary>
    public class AdvertismentAdvancedDto
    {
        /// <summary>
        /// Primary identity <see cref="int"/> ID.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// The title of the advertisment
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// The Username of the user who has posted the 
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// A link to picture
        /// </summary>
        public string ImageUrl { get; set; }

        /// <summary>
        /// The description of the advertisment
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// The year-of-build of the advertismet. Needs to be an <see cref="int"/> of only 4 digits.
        /// </summary>
        public int ConstructionYear { get; set; }

        /// <summary>
        /// The creation date of the advertisment. Maps to a <see cref="DateTimeOffset"/>.
        /// </summary>
        public DateTimeOffset CreatedOn { get; set; }

        /// <summary>
        /// The Selling-Price, can be <see cref="Nullable"/> (can't be <see cref="Nullable"/> if the <see cref="RentingPrice"/> is <see cref="Nullable"/>).
        /// </summary>
        public int? SellingPrice { get; set; }

        /// <summary>
        /// The renting price, can be <see cref="Nullable"/> (can't be <see cref="Nullable"/> if the <see cref="SellingPrice"/> is <see cref="Nullable"/>).
        /// </summary>
        public int? RentingPrice { get; set; }

        /// <summary>
        /// Should be one of the realestate types in the <see cref="Properties"/> table in the DB.
        /// </summary>
        public string RealEstateType { get; set; }

        /// <summary>
        /// <see cref="Boolean"/> True if the propert can be sold. If <see cref="CanBeRented"/> is null. <see cref="CanBeSold"/> can't be null.
        /// </summary>
        public bool CanBeSold { get; set; }

        /// <summary>
        /// <see cref="Boolean"/> True if the property can be rented. If <see cref="CanBeSold"/> is null. <see cref="CanBeRented"/> can't be null.
        /// </summary>
        public bool CanBeRented { get; set; }

        /// <summary>
        /// The correct adrress to the property.
        /// </summary>
        public string Address { get; set; }
    }
}

