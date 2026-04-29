using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

public class NameAttribute : ValidationAttribute
{
    private const string RegexPattern = @"^[A-Za-z\s]{2,25}$";

    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        if (value == null || string.IsNullOrWhiteSpace(value.ToString()))
        {
            return new ValidationResult("Name is required.");
        }

        string name = value.ToString().Trim();

        if (!Regex.IsMatch(name, RegexPattern))
        {
            return new ValidationResult(
                "Name must be 2-25 characters long and can only contain (A-Z, a-z) and spaces."
            );
        }

        if (Regex.IsMatch(name, @"^\s+$"))
        {
            return new ValidationResult("Name cannot consist of spaces only.");
        }

        return ValidationResult.Success;
    }
}