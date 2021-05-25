#region

using System;
using System.ComponentModel.DataAnnotations;

#endregion

namespace comrade.Domain.CustomDataAnnotations
{
    public class IdNotZeroValidation : ValidationAttribute
    {
        protected override ValidationResult IsValid(
            object value,
            ValidationContext validationContext)
        {
            return Convert.ToInt32(value) > 0
                ? ValidationResult.Success
                : new ValidationResult($"{validationContext.DisplayName} must be an integer greater than 0.");
        }
    }
}