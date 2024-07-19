using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using CarWashAPI.Model;

namespace CarWashAPI.DTO
{
    public class PaymentDTO
    {
     
        public int PaymentId { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal TotalAmount { get; set; }

        [Required]
        public DateTime PaymentTime { get; set; }

        [Required]
        [MaxLength(100)]
        public string? PaymentType { get; set; }
        public int? ReceiptId { get; set; }

        public int? UserId { get; set; }

        public int? OrderId { get; set; }

    }
}
