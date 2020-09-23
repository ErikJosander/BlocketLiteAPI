using BlocketLiteAPI.Models;
using System.ComponentModel.DataAnnotations;

namespace BlocketLiteAPI.ValidationAttributes
{
    /// <summary>
    /// A Data Annotation class that validates <see cref="UserForCreationDto.Password"/> and <see cref="UserForCreationDto.ConfirmPassword"/>
    /// </summary>
    public class ConfirmPasswordAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value,
            ValidationContext validationContext)
        {
            var user = (UserForCreationDto)validationContext.ObjectInstance;

            if (user.Password != user.ConfirmPassword)
            {
                return new ValidationResult(
                    "Password and confirmation password needs to be the same.",
                    new[] { nameof(UserForCreationDto) });
            }

            return ValidationResult.Success;
        }
    }
}
