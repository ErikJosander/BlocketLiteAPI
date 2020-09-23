using BlocketLiteAPI.Models;
using System.ComponentModel.DataAnnotations;

namespace BlocketLiteAPI.ValidationAttributes
{
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
