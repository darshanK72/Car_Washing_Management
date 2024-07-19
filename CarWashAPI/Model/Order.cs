using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace CarWashAPI.Model
{
    public class Order
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int OrderId { get; set; }

        [Required]
        public string? Status { get; set; }

        [Required]
        public DateTime? ScheduledDate { get; set; }

        [Required]
        public DateTime? ActualWashDate { get; set; }

        [Required]
        public decimal TotalPrice { get; set; }

        public string? Notes { get; set; }

        public int? UserId { get; set; }

        [ForeignKey("UserId")]
        [JsonIgnore]
        public User? User { get; set; }

        public int? WasherId { get; set; }

        [ForeignKey("WasherId")]
        [JsonIgnore]
        public Washer? Washer { get; set; }

        public int? CarId { get; set; }

        [ForeignKey("CarId")]
        [JsonIgnore]
        public Car? Car { get; set; }
        public int? ReceiptId { get; set; }

        [ForeignKey("ReceiptId")]
        [JsonIgnore]
        public Receipt? Receipt { get; set; }
        public int? PaymentId { get; set; }

        [ForeignKey("PaymentId")]
        [JsonIgnore]
        public Payment? Payment { get; set; }
        public int? PackageId { get; set; }

        [ForeignKey("PackageId")]
        [JsonIgnore]
        public Package? Package { get; set; }
    }
}
