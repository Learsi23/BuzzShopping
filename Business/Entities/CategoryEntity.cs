using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Business.Entities
{
    public class CategoryEntity
    {
        public CategoryEntity()
        {
            Products = new List<ProductEntity>();
        }

        [Key]
        public int CategoryId { get; set; }

        [Required(ErrorMessage = "The Name field is required.")]
        [StringLength(100, ErrorMessage = "The Name must be at most 100 characters long.")]
        public string Name { get; set; } = null!;

        [Required(ErrorMessage = "The Description field is required.")]
        [StringLength(1000, ErrorMessage = "The Description must be at most 1000 characters long.")]
        public string Description { get; set; } = null!;

        // Nuevos campos para mejoras
        [StringLength(255, ErrorMessage = "The Category Image path must be at most 255 characters long.")]
        public string? Image { get; set; } // Ruta de la imagen de la categoría

        [StringLength(255, ErrorMessage = "The SEO URL must be at most 255 characters long.")]
        public string? Slug { get; set; } // URL amigable para SEO (por ejemplo, "electronicos-y-gadgets")

        public bool IsActive { get; set; } // Para habilitar o deshabilitar la categoría

        public ICollection<ProductEntity> Products { get; set; }
    }
}