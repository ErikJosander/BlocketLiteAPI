using BlocketLiteAPI.Models;
using System.ComponentModel.DataAnnotations;

namespace BlocketLiteAPI.ValidationAttributes
{
    public class ConstructionYearMustBeLesserOrEqualToThisYearAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value,
            ValidationContext validationContext)
        {
            var advertisment = (AdvertisementForCreationDto)validationContext.ObjectInstance;

            if (advertisment.ConstructionYear > Helpers.GetCurrentYearHelper.GetCurrentYear())
            {
                return new ValidationResult(
                    "The provided year has not occured yet",
                    new[] { nameof(AdvertisementForCreationDto) });
            }

            return ValidationResult.Success;
        }
    }
}
