using System.ComponentModel.DataAnnotations;

namespace CarWashAPI.DTO
{
    public class RegisterDto
    {
        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 6)]
        public string Password { get; set; }

        [Phone]
        public string PhoneNumber { get; set; }

        [Required]
        [RegularExpression("^(Admin|User|Washer)$", ErrorMessage = "Role must be either Admin, User, or Washer")]
        public string Role { get; set; }
    }
}
