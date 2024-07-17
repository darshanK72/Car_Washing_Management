using System.ComponentModel.DataAnnotations;

namespace CarWashAPI.DTO
{
    public class CarDTO
    {
        [Required]
        public int UserId { get; set; }

        [Required]
        [StringLength(50)]
        public string Make { get; set; }

        [Required]
        [StringLength(50)]
        public string Model { get; set; }

        [Required]
        [Range(1900, 2100)]
        public int Year { get; set; }

        [Required]
        [StringLength(20)]
        public string LicensePlate { get; set; }

        public string ImageUrl { get; set; }

    }
}

