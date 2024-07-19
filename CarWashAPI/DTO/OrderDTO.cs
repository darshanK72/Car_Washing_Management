using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace CarWashAPI.DTO
{
    public class OrderDTO
    {
        [Key]
        public int OrderId { get; set; }

        [Required(ErrorMessage = "User ID is required.")]
        public int? UserId { get; set; }

        [Required(ErrorMessage = "Washer ID is required.")]
        public int? WasherId { get; set; }

        [Required(ErrorMessage = "Receipt ID is required.")]
         public int? ReceiptId { get; set; }

        [Required(ErrorMessage = "Car ID is required.")]
        public int? CarId { get; set; }

        [Required(ErrorMessage = "Package ID is required.")]
        public int? PackageId { get; set; }

        [Required(ErrorMessage = "Status is required.")]
        [StringLength(20, ErrorMessage = "Status length can't be more than 20 characters.")]
        public string Status { get; set; } 

        public DateTime? ScheduledDate { get; set; }

        public DateTime? ActualWashDate { get; set; }

        [Required(ErrorMessage = "Total price is required.")]
        public decimal TotalPrice { get; set; }

        [StringLength(500, ErrorMessage = "Notes length can't be more than 500 characters.")]
        public string Notes { get; set; }

        public int? ReviewId { get; set; }
    }
}
