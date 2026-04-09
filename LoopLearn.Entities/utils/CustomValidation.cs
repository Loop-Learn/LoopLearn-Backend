using LoopLearn.Entities.DTO;
using System.ComponentModel.DataAnnotations;

namespace LoopLearn.Entities.utils
{
    public static class CustomValidation
    {
        public static ValidationResult BirthDate(object value, ValidationContext context)
        {
            if (value is not DateTime date)
                return new ValidationResult("Invalid date.");

            if (date > DateTime.Today)
                return new ValidationResult("Date cannot be in the future.");

            int age = DateTime.Today.Year - date.Year;

            if (date.Date > DateTime.Today.AddYears(-age))
                age--;

            if (age < 6)
                return new ValidationResult("You must be at least 6 years old.");

            return ValidationResult.Success!;
        }
        public static ValidationResult EmailOrUsername(object value, ValidationContext context)
        {
            var dto = (LoginDTO)context.ObjectInstance;

            bool hasUsername = !string.IsNullOrWhiteSpace(dto.Username);
            bool hasEmail = !string.IsNullOrWhiteSpace(dto.Email);

            if (hasUsername == hasEmail)
            {
                return new ValidationResult(
                    "Provide either Username or Email (not both or none).",
                    new[] { nameof(dto.Username), nameof(dto.Email) }
                );
            }

            return ValidationResult.Success!;
        }
    }
}
