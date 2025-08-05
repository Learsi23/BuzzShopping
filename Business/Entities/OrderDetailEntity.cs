using Business.Entities;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Business.Entities
{
    public class OrderDetailEntity
    {
        [Key]
        public int OrderDetailId { get; set; }

        public int ProductId { get; set; }

        [ForeignKey("ProductId")]
        public ProductEntity Product { get; set; } = null!;

        public int OrderId { get; set; }
        [ForeignKey("OrderId")]
        public OrderEntity Order { get; set; } = null!;

        public int Amount { get; set; }

        [Precision(18, 2)]
        [Required(ErrorMessage = "The Unit Price at Purchase field is required.")]
        public decimal UnitPriceAtPurchase { get; set; }

        [Precision(18, 2)]
        public decimal DiscountApplied { get; set; }

        [Precision(18, 2)]
        [Required(ErrorMessage = "The Line Total field is required.")]
        public decimal LineTotal { get; set; }

        public DateTime CreatedDate { get; set; }

        // Campo para el estado de la línea de pedido
        // Los posibles estados podrían ser "Pending", "Shipped", "Delivered", "Canceled", etc.
        [StringLength(50, ErrorMessage = "The status must be at most 50 characters long.")]
        public string Status { get; set; } = "Pending"; // Valor por defecto
    }
}