using System.ComponentModel.DataAnnotations;

namespace Business.DTOs
{
    public class OrderDetailDto
    {
        [Required(ErrorMessage = "The Product ID is required.")]
        public int ProductId { get; set; }

        [Required(ErrorMessage = "The amount is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "The amount must be at least 1.")]
        public int Amount { get; set; }
    }
}