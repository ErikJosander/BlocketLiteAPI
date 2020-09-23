﻿using BlocketLiteAPI.Models;
using System.ComponentModel.DataAnnotations;

namespace BlocketLiteAPI.ValidationAttributes
{
    public class SellingAndRentalPriceCantBothBeNullAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value,
            ValidationContext validationContext)
        {
            var advertisment = (AdvertisementForCreationDto)validationContext.ObjectInstance;

            if (advertisment.SellingPrice == null && advertisment.RentingPrice == null)
            {
                return new ValidationResult(
                    "Both selling price and rental price can't be null.",
                    new[] { nameof(AdvertisementForCreationDto) });
            }

            return ValidationResult.Success;
        }
    }
}
