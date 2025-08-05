using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Business.Entities
{
    // Enum para los estados del pedido
    public enum OrderStatus
    {
        Pending,
        Processing,
        Shipped,
        Delivered,
        Cancelled
    }

    public class OrderEntity
    {
        [Key]
        public int OrderId { get; set; }

        [Required(ErrorMessage = "The OrderNumber field is required.")]
        [StringLength(50)]
        public string OrderNumber { get; set; } = null!;

        [Required(ErrorMessage = "The UserId field is required.")]
        public int UserId { get; set; }

        [ForeignKey("UserId")]
        public UserEntity User { get; set; } = null!;

        // Campos para capturar los datos del usuario al momento del pedido
        [Required(ErrorMessage = "The CustomerName field is required.")]
        [StringLength(100)]
        public string CustomerName { get; set; } = null!;

        [Required(ErrorMessage = "The CustomerEmail field is required.")]
        [StringLength(255)]
        public string CustomerEmail { get; set; } = null!;

        [StringLength(20)]
        public string? CustomerPhone { get; set; }

        // Fechas importantes del ciclo de vida del pedido
        [Required(ErrorMessage = "The CreatedDate field is required.")]
        public DateTime CreatedDate { get; set; }
        public DateTime? PaymentDate { get; set; }
        public DateTime? ShippedDate { get; set; }

        // Uso de un enum para el estado del pedido
        [Required(ErrorMessage = "The State field is required.")]
        public OrderStatus Status { get; set; }

        // Direcciones de envío y facturación
        [Required(ErrorMessage = "The ShippingAddressId field is required.")]
        public int ShippingAddressId { get; set; }
        [ForeignKey("ShippingAddressId")]
        public AddressEntity ShippingAddress { get; set; } = null!;

        public int? BillingAddressId { get; set; }
        [ForeignKey("BillingAddressId")]
        public AddressEntity? BillingAddress { get; set; }

        // Campos para el total del pedido
        [Precision(18, 2)]
        [Required(ErrorMessage = "The Subtotal field is required.")]
        public decimal Subtotal { get; set; }

        [Precision(18, 2)]
        public decimal ShippingCost { get; set; }

        [Precision(18, 2)]
        public decimal DiscountAmount { get; set; }

        [Precision(18, 2)]
        [Required(ErrorMessage = "The Total field is required.")]
        public decimal Total { get; set; }

        public ICollection<OrderDetailEntity> OrderDetails { get; set; } = null!;
    }
}