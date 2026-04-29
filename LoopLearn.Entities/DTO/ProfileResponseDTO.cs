using LoopLearn.Entities.utils.CustomValidations;
using System.ComponentModel.DataAnnotations;
using static LoopLearn.Entities.utils.Enums;

namespace LoopLearn.Entities.DTO
{
    public class ProfileResponseDTO
    {
        public string Username { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public DateTime BirthDate { get; set; }
        public Gender Gender { get; set; }
        public bool IsVerifiedEmail { get; set; } 
        public DateTime JoinDate { get; set; }
        public string Avatar { get; set; }
    }
    public class ProfileUpdateDTO
    {
        [Name]
        public string? FirstName { get; set; }
        [Name]
        public string? LastName { get; set; }
        [EmailAddress]
        public string? Email { get; set; }
        [RegularExpression(@"^01[0125]\d{8}$", ErrorMessage = "Phone Number is not Valid. Please make sure its an EGY phone Number.")]
        public string? Phone { get; set; }
        [AvatarUrl]
        public string? Avatar { get; set; }
    }
    public class ChangePasswordDTO
    {
        [Required(ErrorMessage = "Old password is required")]
        [PasswordValidation]
        public string OldPassword { get; set; }

        [Required(ErrorMessage = "New password is required")]
        [DataType(DataType.Password)]
        [PasswordValidation]
        public string NewPassword { get; set; }
    }
}
