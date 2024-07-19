using CarWashAPI.Model;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CarWashAPI.DTO
{
    public class WashRequest
    {
        public int OrderId { get; set; }
        public string? Status { get; set; }

        public DateTime? ScheduledDate { get; set; }
        public DateTime? ActualWashDate { get; set; }
        public decimal TotalPrice { get; set; }
        public string? Notes { get; set; }

        public int? UserId { get; set; }
        public string? Address { get; set; }
    }
}
