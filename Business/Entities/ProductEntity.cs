using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Business.Entities
{
    public class ProductEntity
    {
        [Key]
        public int ProductId { get; set; }

        [Required(ErrorMessage = "The Code field is required.")]
        [StringLength(50, ErrorMessage = "The Code must be at most 50 characters long.")]
        public string Code { get; set; } = null!;

        [Required(ErrorMessage = "The Name field is required.")]
        [StringLength(255, ErrorMessage = "The Name must be at most 255 characters long.")]
        public string Name { get; set; } = null!;

        [Required(ErrorMessage = "The Model field is required.")]
        [StringLength(255, ErrorMessage = "The Model must be at most 255 characters long.")]
        public string Model { get; set; } = null!;

        [Required(ErrorMessage = "The Description field is required.")]
        [StringLength(1000, ErrorMessage = "The Description must be at most 1000 characters long.")]
        public string Description { get; set; } = null!;

        [Required(ErrorMessage = "The Price field is required.")]
        [Precision(18, 2)]
        public decimal Price { get; set; }

        // Nuevos campos para descuentos y ofertas
        [Precision(18, 2)]
        public decimal? SpecialOfferPrice { get; set; }

        [Range(0, 100, ErrorMessage = "Discount must be between 0 and 100 percent.")]
        public int? DiscountPercentage { get; set; }

        public bool HasSpecialOffer { get; set; }

        [Required(ErrorMessage = "The Image field is required.")]
        [StringLength(255, ErrorMessage = "The Image path must be at most 255 characters long.")]
        public string Image { get; set; } = null!; 

        [StringLength(255, ErrorMessage = "The Product URL must be at most 255 characters long.")]
        public string? ProductUrl { get; set; }

        [Required(ErrorMessage = "The Category field is required.")]
        public int CategoryId { get; set; }

        [ForeignKey("CategoryId")]
        public CategoryEntity Category { get; set; } = null!;

        [Required(ErrorMessage = "The Stock field is required.")]
        public int Stock { get; set; }

        public bool Active { get; set; }

        public bool IsFeatured { get; set; }

        [Required(ErrorMessage = "The Brand field is required.")]
        [StringLength(100, ErrorMessage = "The Brand must be at most 100 characters long.")]
        public string Brand { get; set; } = null!;

        public DateTime CreatedDate { get; set; }
        public DateTime? LastUpdatedDate { get; set; }

        public ICollection<OrderDetailEntity> OrderDetails { get; set; } = null!;
    }
}