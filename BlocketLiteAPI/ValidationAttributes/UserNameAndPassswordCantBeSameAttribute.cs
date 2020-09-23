using BlocketLiteAPI.Models;
using System.ComponentModel.DataAnnotations;

namespace BlocketLiteAPI.ValidationAttributes
{
    public class UserNameAndPassswordCantBeSameAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value,
            ValidationContext validationContext)
        {
            var user = (UserForCreationDto)validationContext.ObjectInstance;

            if (user.UserName == user.Password)
            {
                return new ValidationResult(
                    "Username and password can not be the same.",
                    new[] { nameof(UserForCreationDto) });
            }

            return ValidationResult.Success;
        }
    }
}
