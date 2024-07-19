using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace CarWashAPI.Model
{
    public class Washer
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int WasherId { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        [Phone]
        public string PhoneNumber { get; set; }

        [Required]
        public string Role { get; set; } = "Washer";

        [Url]
        public string? ProfilePicture { get; set; }

        public bool? IsActive {  get; set; }

        [StringLength(200)]
        public string? Address { get; set; }

        [JsonIgnore]
        public ICollection<Order>? Orders { get; set; }

        [JsonIgnore]
        public ICollection<Review>? Reviews { get; set; }
    }
}