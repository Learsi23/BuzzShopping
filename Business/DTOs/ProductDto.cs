using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Business.DTOs
{
    public class ProductDto
    {
        [Required(ErrorMessage = "El código es un campo requerido.")]
        [StringLength(50, ErrorMessage = "El código no puede tener más de 50 caracteres.")]
        public string Code { get; set; } = null!;

        [Required(ErrorMessage = "El nombre es un campo requerido.")]
        [StringLength(255, ErrorMessage = "El nombre no puede tener más de 255 caracteres.")]
        public string Name { get; set; } = null!;

        [Required(ErrorMessage = "El modelo es un campo requerido.")]
        [StringLength(255, ErrorMessage = "El modelo no puede tener más de 255 caracteres.")]
        public string Model { get; set; } = null!;

        [Required(ErrorMessage = "La descripción es un campo requerido.")]
        [StringLength(1000, ErrorMessage = "La descripción no puede tener más de 1000 caracteres.")]
        public string Description { get; set; } = null!;

        [Required(ErrorMessage = "El precio es un campo requerido.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "El precio debe ser un valor positivo.")]
        public decimal Price { get; set; }

        public decimal? SpecialOfferPrice { get; set; }

        [Range(0, 100, ErrorMessage = "El descuento debe estar entre 0 y 100.")]
        public int? DiscountPercentage { get; set; }

        public bool HasSpecialOffer { get; set; }

        public IFormFile? ImageFile { get; set; }

        [StringLength(255, ErrorMessage = "The Product URL must be at most 255 characters long.")]
        public string? ProductUrl { get; set; } // ¡Campo añadido aquí!

        [Required(ErrorMessage = "La categoría es un campo requerido.")]
        public int CategoryId { get; set; }

        [Required(ErrorMessage = "El stock es un campo requerido.")]
        [Range(0, int.MaxValue, ErrorMessage = "El stock debe ser un valor positivo.")]
        public int Stock { get; set; }

        public bool Active { get; set; }

        public bool IsFeatured { get; set; }

        [Required(ErrorMessage = "La marca es un campo requerido.")]
        [StringLength(100, ErrorMessage = "La marca no puede tener más de 100 caracteres.")]
        public string Brand { get; set; } = null!;
    }
}