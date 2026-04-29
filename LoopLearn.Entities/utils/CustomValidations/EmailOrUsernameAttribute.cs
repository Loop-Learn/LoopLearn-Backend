using LoopLearn.Entities.DTO;
using System.ComponentModel.DataAnnotations;

public class EmailOrUsernameAttribute : ValidationAttribute
{
    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        var dto = (LoginDTO)validationContext.ObjectInstance;

        bool hasUsername = !string.IsNullOrWhiteSpace(dto.Username);
        bool hasEmail = !string.IsNullOrWhiteSpace(dto.Email);

        if (hasUsername == hasEmail)
        {
            return new ValidationResult(
                "Provide either Username or Email (not both or none).",
                new[] { nameof(dto.Username), nameof(dto.Email) }
            );
        }

        return ValidationResult.Success;
    }
}