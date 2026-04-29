using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

public class AvatarUrlAttribute : ValidationAttribute
{
    private const int MaxLength = 500;
    private const string ImageExtensionsPattern = @"\.(jpg|jpeg|png|gif|webp)(\?.*)?$";

    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        if (value == null || string.IsNullOrWhiteSpace(value.ToString()))
            return ValidationResult.Success; 

        string url = value.ToString();

        if (url.Length > MaxLength)
            return new ValidationResult($"Avatar URL cannot exceed {MaxLength} characters.");

        if (!Uri.TryCreate(url, UriKind.Absolute, out Uri? uriResult) ||
            (uriResult.Scheme != Uri.UriSchemeHttp && uriResult.Scheme != Uri.UriSchemeHttps))
        {
            return new ValidationResult("Avatar URL must be a valid absolute HTTP/HTTPS URL.");
        }

        if (!Regex.IsMatch(url, ImageExtensionsPattern, RegexOptions.IgnoreCase))
        {
            return new ValidationResult("Avatar URL must point to an image file (jpg, jpeg, png, gif, webp).");
        }

        return ValidationResult.Success;
    }
}