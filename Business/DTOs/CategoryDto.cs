using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Business.DTOs
{
    public class CategoryDto
    {
        [Required(ErrorMessage = "El nombre es un campo requerido.")]
        [StringLength(100, ErrorMessage = "El nombre no puede tener más de 100 caracteres.")]
        public string Name { get; set; } = null!;

        [Required(ErrorMessage = "La descripción es un campo requerido.")]
        [StringLength(1000, ErrorMessage = "La descripción no puede tener más de 1000 caracteres.")]
        public string Description { get; set; } = null!;

        // Propiedad para recibir el archivo de imagen desde el formulario
        public IFormFile? ImageFile { get; set; }

        [StringLength(255, ErrorMessage = "El Slug (URL) no puede tener más de 255 caracteres.")]
        public string? Slug { get; set; }

        public bool IsActive { get; set; }
    }
}