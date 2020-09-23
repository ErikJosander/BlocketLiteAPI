using BlocketLiteAPI.Models;
using BlocketLiteEFCoreDB.Entities;
using System.ComponentModel.DataAnnotations;

namespace BlocketLiteAPI.ValidationAttributes
{
    /// <summary>
    /// A Data Annotation class that validates <see cref="Advertisement.Title"/> and <see cref="Advertisement.Description"/>
    /// </summary>
    public class AdvertismentHeadingMustBeDifferentFromDescriptionAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value,
            ValidationContext validationContext)
        {
            var advertisment = (AdvertisementForCreationDto)validationContext.ObjectInstance;

            if (advertisment.Title == advertisment.Description)
            {
                return new ValidationResult(
                    "The provided Description should be different than the Heading",
                    new[] { nameof(AdvertisementForCreationDto) });
            }

            return ValidationResult.Success;
        }
    }
}
