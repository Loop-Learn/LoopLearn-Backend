using LoopLearn.Entities.utils;
using System.ComponentModel.DataAnnotations;

namespace LoopLearn.Entities.DTO
{
    [CustomValidation(typeof(CustomValidation),nameof(CustomValidation.EmailOrUsername))]
    public class LoginDTO
    {
        [RegularExpression(@"^(?!\d+$)[a-zA-Z0-9_]{3,25}$")]
        public string? Username { get; set; }
        [RegularExpression(@"^[^@\s]+@[^@\s]+\.[^@\s]+$", ErrorMessage = "Email address is not Valid.")]
        public string? Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$%!*?&]).{8,256}$",ErrorMessage ="Invalid Password")]
        public string Password { get; set; }
    }
}
