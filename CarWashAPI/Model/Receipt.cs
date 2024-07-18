using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace CarWashAPI.Model
{
    public class Receipt
    {
        [Key]
        public int ReceiptId { get; set; }

        [Required]
        public DateTime WashingDate { get; set; }

        [Required]
        public decimal Amount { get; set; }

        public string? PaymentMethod { get; set; }

        [Required]
        public string TransactionId { get; set; }

        [Required]
        public string Status { get; set; }
    }
}
