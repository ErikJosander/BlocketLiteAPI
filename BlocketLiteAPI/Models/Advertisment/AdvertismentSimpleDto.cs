using System;

namespace BlocketLiteAPI.Models.Advertisment
{
    /// <summary>
    /// A model class for mapping <see cref="Advertisment"/> to an XML-string
    /// </summary>
    public class AdvertismentSimpleDto
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
        /// Link to a picture
        /// </summary>
        public string ImageUrl { get; set; }

        /// <summary>
        /// The Selling-Price, can be <see cref="Nullable"/> (can't be <see cref="Nullable"/> if the <see cref="RentingPrice"/> is <see cref="Nullable"/>).
        /// </summary>
        public int? SellingPrice { get; set; }

        /// <summary>
        /// The renting price, can be <see cref="Nullable"/> (can't be <see cref="Nullable"/> if the <see cref="SellingPrice"/> is <see cref="Nullable"/>).
        /// </summary>
        public int? RentingPrice { get; set; }

        /// <summary>
        /// <see cref="Boolean"/> True if the propert can be sold. If <see cref="CanBeRented"/> is null. <see cref="CanBeSold"/> can't be null.
        /// </summary>
        public bool CanBeSold { get; set; }

        /// <summary>
        /// <see cref="Boolean"/> True if the property can be rented. If <see cref="CanBeSold"/> is null. <see cref="CanBeRented"/> can't be null.
        /// </summary>
        public bool CanBeRented { get; set; }
    }
}
