using LoopLearn.Entities.utils;
using LoopLearn.Entities.utils.CustomValidations;
using System.ComponentModel.DataAnnotations;

namespace LoopLearn.Entities.DTO
{
    [EmailOrUsername]
    public class LoginDTO
    {
        [Username]
        public string? Username { get; set; }
        [EmailAddress]
        public string? Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [PasswordValidation]
        public string Password { get; set; }
    }
}
