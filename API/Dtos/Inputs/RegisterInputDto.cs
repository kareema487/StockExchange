using System.ComponentModel.DataAnnotations;

namespace API.Dtos.Inputs
{
    public class RegisterInputDto
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; } = null!;
        [Required]
        [RegularExpression("^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)(?=.*[#$^+=!*()@%&]).{8,}$",
            ErrorMessage = "Password must have 1 uppercase,1 lowercase, 1 number, 1 non alphanumeric and at least 8 characters")]
        public string Password { get; set; } = null!;
    }
}
