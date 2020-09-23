using BlocketLiteAPI.Models;
using System.ComponentModel.DataAnnotations;

namespace BlocketLiteAPI.ValidationAttributes
{
    /// <summary>
    /// A Data Annotation class that validates that <see cref="UserForCreationDto.UserName"/> and <see cref="UserForCreationDto.Password"/> is not equal.
    /// </summary>
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
