using BlocketLiteEFCoreDB.Entities;
using System.ComponentModel.DataAnnotations;

namespace BlocketLiteEFCoreDB.DbValidationAttributes
{
    public class RentingAndSellingCanNotBothBeNullAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value,
            ValidationContext validationContext)

        {
            var advertisment = (Advertisement)validationContext.ObjectInstance;

            if (advertisment.CanBeRented == false && advertisment.CanBeSold == false)
            {
                return new ValidationResult(
                    "SellingPrice and RentingPrice can not both be false.",
                    new[] { nameof(Advertisement) });
            }
            return ValidationResult.Success;
        }
    }
}
