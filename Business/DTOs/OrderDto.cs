using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Business.DTOs
{
    public class OrderDto
    {
        [Required(ErrorMessage = "The User ID is required.")]
        public int UserId { get; set; }

        [Required(ErrorMessage = "The Shipping Address ID is required.")]
        public int ShippingAddressId { get; set; }

        public int? BillingAddressId { get; set; }

        // Este DTO recibe los productos y cantidades desde el carrito
        [Required(ErrorMessage = "At least one product is required for the order.")]
        public ICollection<OrderDetailDto> OrderDetails { get; set; } = null!;
    }
}