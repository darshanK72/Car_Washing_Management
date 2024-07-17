using System.ComponentModel.DataAnnotations;

namespace CarWashAPI.DTO
{
    public class PackageDto
    {
        public int PackageId { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The Name field must be a maximum length of 100 characters.")]
        public string Name { get; set; }

        [StringLength(500, ErrorMessage = "The Description field must be a maximum length of 500 characters.")]
        public string Description { get; set; }

        [Required]
        [Range(0, double.MaxValue, ErrorMessage = "The Price must be a positive value.")]
        public decimal Price { get; set; }
    }
}
