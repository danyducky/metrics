using System.ComponentModel.DataAnnotations;
using Metrics.Api.Models.User;

namespace Metrics.Api.Validation
{
    public class GreaterDateAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var model = (UserDateModel)validationContext.ObjectInstance;

            return model.RegistrationDate > model.LastActivityDate ? 
                new ValidationResult("Last activity date must be greater than registration date") 
                : 
                ValidationResult.Success;
        }
    }
}