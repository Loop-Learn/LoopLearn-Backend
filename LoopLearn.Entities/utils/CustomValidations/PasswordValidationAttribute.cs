using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace LoopLearn.Entities.utils.CustomValidations
{
    public class PasswordValidationAttribute : ValidationAttribute
    {
        private static readonly (string Pattern, string Message)[] Requirements = new[]
    {
        (@"[a-z]", "at least one lowercase letter"),
        (@"[A-Z]", "at least one uppercase letter"),
        (@"\d", "at least one digit"),
        (@"[@$%!*?&]", "at least one special character (@$%!*?&)"),
        (@"^.{8,256}$", "length between 8 and 256 characters")
    };

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null || string.IsNullOrWhiteSpace(value.ToString()))
            {
                return new ValidationResult("Password is required.");
            }

            string password = value.ToString();
            var missingRequirements = new List<string>();

            foreach (var req in Requirements)
            {
                if (!Regex.IsMatch(password, req.Pattern))
                    missingRequirements.Add(req.Message);
            }

            if (missingRequirements.Any())
            {
                string message = missingRequirements.Count == 1
                    ? $"Password must contain {missingRequirements[0]}."
                    : $"Password must contain: {string.Join(", ", missingRequirements)}.";

                return new ValidationResult(message);
            }

            return ValidationResult.Success;
        }
    }
}
