using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace CarWashAPI.Model
{
    public class Car
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CarId { get; set; }

        [Required]
        [StringLength(50)]
        public string? Make { get; set; }

        [Required]
        [StringLength(50)]
        public string? Model { get; set; }

        [Required]
        public int Year { get; set; }

        [Required]
        [StringLength(20)]
        public string? LicensePlate { get; set; }

        [Url]
        public string? ImageUrl { get; set; }
        public int? UserId { get; set; }

        [ForeignKey("UserId")]
        [JsonIgnore]
        public User? User { get; set; }

        [JsonIgnore]
        public ICollection<Order>? Orders { get; set; }
    }
}