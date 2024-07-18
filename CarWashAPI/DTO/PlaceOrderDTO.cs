using System.ComponentModel.DataAnnotations;

namespace CarWashAPI.DTO
{
    public class PlaceOrderDTO
    {

        [Required]
        public int UserId { get; set; }

        [Required]
        public int CarId { get; set; }

        [Required]
        public int PackageId { get; set; }

        [Required]
        public bool ScheduleNow { get; set; }

        public DateTime? ScheduledDate { get; set; }

        [StringLength(500)]
        public string Notes { get; set; }
    }
}
