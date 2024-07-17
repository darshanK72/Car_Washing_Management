using System.ComponentModel.DataAnnotations;

namespace CarWash2.DTO
{
    public class ReviewDto
    {
        public int ReviewId { get; set; }

        [Required(ErrorMessage = "UserId is required.")]
        public int UserId { get; set; }

        [Required(ErrorMessage = "WasherId is required.")]
        public int WasherId { get; set; }

        [Required(ErrorMessage = "OrderId is required.")]
        public int OrderId { get; set; }

        [Required(ErrorMessage = "Rating is required.")]
        [Range(1, 5, ErrorMessage = "Rating must be between 1 and 5.")]
        public int Rating { get; set; }

        [StringLength(1000, ErrorMessage = "Comment can't be longer than 1000 characters.")]
        public string Comment { get; set; }
    }
}
