using System.ComponentModel.DataAnnotations;
using LoopLearn.Entities.utils;
using static LoopLearn.Entities.utils.Enums;

namespace LoopLearn.Entities.DTO
{
    public class RegisterDTO
    {
        [Required,StringLength(20,MinimumLength =3)]
        [RegularExpression(@"^[a-zA-Z]+$")]
        public string FName { get; set; }

        [Required, StringLength(20, MinimumLength = 3)]
        [RegularExpression(@"^[a-zA-Z]+$")]
        public string LName { get; set; }

        [Required, StringLength(20, MinimumLength = 3)]
        [RegularExpression(@"^(?!\d+$)[a-zA-Z0-9_]{3,25}$")]
        public string Username { get; set; }

        [Required, StringLength(100)]
        [RegularExpression(@"^[^@\s]+@[^@\s]+\.[^@\s]+$", ErrorMessage = "Email address is not Valid.")]
        public string Email { get; set; }

        [Required, StringLength(256, MinimumLength = 8)]
        [DataType(DataType.Password)]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$%!*?&]).{8,256}$")]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Compare(nameof(Password), ErrorMessage = "Passwords do not match.")]
        public string ConfirmPassword { get; set; }

        [Required, StringLength(11, ErrorMessage = "Phone Number must be 11 digits")]
        [RegularExpression(@"^01[0125]\d{8}$", ErrorMessage = "Phone Number is not Valid.Please make sure its an EGY phone Number")]
        public string Phone { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [CustomValidation(typeof(CustomValidation),nameof(CustomValidation.BirthDate))]
        public DateTime BirthDate { get; set; }

        [Required]
        public Gender Gender { get; set; }

        //[Required]
        //public RoleType Role { get; set; } = RoleType.Student;

    }
}
